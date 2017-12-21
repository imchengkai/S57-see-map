

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
#if !DotSpatialProjections
using ProjNet.Converters.WellKnownText;
using ProjNet.CoordinateSystems;
#else
using DotSpatial.Projections;
#endif
using EasyMap.Geometries;
using EasyMap.Utilities.SpatialIndexing;

namespace EasyMap.Data.Providers
{
    public enum ShapeType
    {
        Null = 0,
        Point = 1,
        PolyLine = 3,
        Polygon = 5,
        Multipoint = 8,
        PointZ = 11,
        PolyLineZ = 13,
        PolygonZ = 15,
        MultiPointZ = 18,
        PointM = 21,
        PolyLineM = 23,
        PolygonM = 25,
        MultiPointM = 28,
        MultiPatch = 31
    } ;

    public class ShapeFile : FilterProvider, IProvider
    {



#if !DotSpatialProjections
        private ICoordinateSystem _coordinateSystem;
#else
		private ProjectionInfo _coordinateSystem;
#endif
        private bool _coordsysReadFromFile;

        private int _fileSize;
        private BoundingBox _envelope;
        private int _featureCount;
        private bool _fileBasedIndex;
        private readonly bool _fileBasedIndexWanted;
        private string _filename;
        private FilterMethod _filterDelegate;
        private bool _isOpen;
        private ShapeType _shapeType;
        private int _srid = -1;
        private BinaryReader _brShapeFile;
        private BinaryReader _brShapeIndex;
        protected DbaseReader DbaseFile;
        private Stream _fsShapeFile;

#if USE_MEMORYMAPPED_FILE
        private static Dictionary<string,System.IO.MemoryMappedFiles.MemoryMappedFile> _memMappedFiles;
        private static Dictionary<string, int> _memMappedFilesRefConter;
        private bool _haveRegistredForUsage = false;
        private bool _haveRegistredForShxUsage = false;
        static ShapeFile()
        {
            _memMappedFiles = new Dictionary<string, System.IO.MemoryMappedFiles.MemoryMappedFile>();
            _memMappedFilesRefConter = new Dictionary<string, int>();
            SpatialIndexCreationOption = SpatialIndexCreation.Recursive;
        }
#else
        static ShapeFile()
        {
            SpatialIndexCreationOption = SpatialIndexCreation.Recursive;
        }
#endif
        private Stream _fsShapeIndex;
        private readonly bool _useMemoryCache;
        private DateTime _lastCleanTimestamp = DateTime.Now;
        private readonly TimeSpan _cacheExpireTimeout = TimeSpan.FromMinutes(1);
        private readonly Dictionary<uint, FeatureDataRow> _cacheDataTable = new Dictionary<uint, FeatureDataRow>();

        private int[] _offsetOfRecord;

        private QuadTree _tree;

        public ShapeFile(string filename)
            : this(filename, false)
        {
        }

        private void CleanInternalCache(IList<uint> objectlist)
        {
            if (_useMemoryCache &&
                DateTime.Now.Subtract(_lastCleanTimestamp) > _cacheExpireTimeout)
            {
                var notIntersectOid = new Collection<uint>();

                foreach (uint oid in _cacheDataTable.Keys)
                {
                    if (!objectlist.Contains(oid))
                    {
                        notIntersectOid.Add(oid);
                    }
                }

                foreach (uint oid in notIntersectOid)
                {
                    _cacheDataTable.Remove(oid);
                }

                _lastCleanTimestamp = DateTime.Now;
            }
        }

        public ShapeFile(string filename, bool fileBasedIndex)
        {
            _filename = filename;
            _fileBasedIndexWanted = fileBasedIndex;
            _fileBasedIndex = (fileBasedIndex) && File.Exists(Path.ChangeExtension(filename, ".shx"));

            string dbffile = Path.ChangeExtension(filename, ".dbf");
            if (File.Exists(dbffile))
                DbaseFile = new DbaseReader(dbffile);

            _useMemoryCache = false;
            ParseHeader();
            ParseProjection();
        }

        public ShapeFile(string filename, bool fileBasedIndex, bool useMemoryCache)
            : this(filename, fileBasedIndex)
        {
            _useMemoryCache = useMemoryCache;
        }

#if !DotSpatialProjections
        public ICoordinateSystem CoordinateSystem
#else
		public ProjectionInfo CoordinateSystem
#endif
        {
            get { return _coordinateSystem; }
            set
            {
                if (_coordsysReadFromFile)
                    throw new ApplicationException("Coordinate system is specified in projection file and is read only");
                _coordinateSystem = value;
            }
        }


        public ShapeType ShapeType
        {
            get { return _shapeType; }
        }


        public string Filename
        {
            get { return _filename; }
            set
            {
                if (value != _filename)
                {
                    if (IsOpen)
                        throw new ApplicationException("Cannot change filename while datasource is open");

                    _filename = value;
                    _fileBasedIndex = (_fileBasedIndexWanted) && File.Exists(Path.ChangeExtension(value, ".shx"));

                    var dbffile = Path.ChangeExtension(value, ".dbf");
                    if (File.Exists(dbffile))
                        DbaseFile = new DbaseReader(dbffile);

                    ParseHeader();
                    ParseProjection();
                    _tree = null;
                }
            }
        }

        public Encoding Encoding
        {
            get { return DbaseFile.Encoding; }
            set { DbaseFile.Encoding = value; }
        }


        #region Disposers and finalizers

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Close();
                    _envelope = null;
                    _tree = null;
#if USE_MEMORYMAPPED_FILE
                    if (_memMappedFilesRefConter.ContainsKey(_filename))
                    {
                        _memMappedFilesRefConter[_filename]--;
                        if (_memMappedFilesRefConter[_filename] == 0)
                        {
                            _memMappedFiles[_filename].Dispose();
                            _memMappedFiles.Remove(_filename);
                            _memMappedFilesRefConter.Remove(_filename);
                        }
                    }
                    string shxFile = Path.ChangeExtension(_filename,".shx");
                    if (_memMappedFilesRefConter.ContainsKey(shxFile))
                    {
                        _memMappedFilesRefConter[shxFile]--;
                        if (_memMappedFilesRefConter[shxFile] <= 0)
                        {
                            _memMappedFiles[shxFile].Dispose();
                            _memMappedFilesRefConter.Remove(shxFile);
                            _memMappedFiles.Remove(shxFile);

                        }
                    }
#endif
                }
                _disposed = true;
            }
        }

        ~ShapeFile()
        {
            Dispose();
        }

        #endregion

        #region IProvider Members

        public void Open()
        {

            if (!_isOpen)
            {
                string shxFile = Path.ChangeExtension(_filename, "shx");
                if (File.Exists(shxFile))
                {
#if USE_MEMORYMAPPED_FILE
                    _fsShapeIndex = CheckCreateMemoryMappedStream(shxFile, ref _haveRegistredForShxUsage);
#else
                    _fsShapeIndex = new FileStream(shxFile, FileMode.Open, FileAccess.Read);
#endif
                    _brShapeIndex = new BinaryReader(_fsShapeIndex, Encoding.Unicode);
                }
#if USE_MEMORYMAPPED_FILE

                _fsShapeFile = CheckCreateMemoryMappedStream(_filename, ref _haveRegistredForUsage);
#else
                _fsShapeFile = new FileStream(_filename, FileMode.Open, FileAccess.Read);
#endif
                _brShapeFile = new BinaryReader(_fsShapeFile);
                _offsetOfRecord = new int[_featureCount];
                PopulateIndexes();
                InitializeShape(_filename, _fileBasedIndex);
                if (DbaseFile != null)
                    DbaseFile.Open();
                _isOpen = true;

            }
        }
#if USE_MEMORYMAPPED_FILE
        private Stream CheckCreateMemoryMappedStream(string filename, ref bool haveRegistredForUsage)
        {
            if (!_memMappedFiles.ContainsKey(filename))
            {
                System.IO.MemoryMappedFiles.MemoryMappedFile memMappedFile = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateFromFile(filename, FileMode.Open);
                _memMappedFiles.Add(filename, memMappedFile);
            }
            if (!haveRegistredForUsage)
            {
                if (_memMappedFilesRefConter.ContainsKey(filename))
                    _memMappedFilesRefConter[filename]++;
                else
                    _memMappedFilesRefConter.Add(filename, 1);

                haveRegistredForUsage = true;
            }

            return _memMappedFiles[filename].CreateViewStream();
        }
#endif

        public void Close()
        {
            if (!_disposed)
            {
                if (_isOpen)
                {
                    _brShapeFile.Close();
                    _fsShapeFile.Close();
                    if (_brShapeIndex != null)
                    {
                        _brShapeIndex.Close();
                        _fsShapeIndex.Close();
                    }

                    _offsetOfRecord = null;

                    if (DbaseFile != null)
                        DbaseFile.Close();
                    _isOpen = false;
                }
            }
        }

        public bool IsOpen
        {
            get { return _isOpen; }
        }

        public Collection<Geometry> GetGeometriesInView(BoundingBox bbox)
        {
            Collection<uint> objectlist = GetObjectIDsInView(bbox);
            if (objectlist.Count == 0) //no features found. Return an empty set
                return new Collection<Geometry>();

            Collection<Geometry> geometries = new Collection<Geometry>();

            for (int i = 0; i < objectlist.Count; i++)
            {
                Geometry g = GetGeometryByID(objectlist[i]);
                if (g != null)
                    geometries.Add(g);
            }

            CleanInternalCache(objectlist);
            return geometries;
        }

        public void ExecuteIntersectionQuery(BoundingBox bbox, FeatureDataSet ds)
        {
            Collection<uint> objectlist = GetObjectIDsInView(bbox);
            FeatureDataTable dt = DbaseFile.NewTable;

            for (int i = 0; i < objectlist.Count; i++)
            {
                FeatureDataRow fdr = GetFeature(objectlist[i], dt);
                if (fdr != null) dt.AddRow(fdr);

                /*
                FeatureDataRow fdr = dbaseFile.GetFeature(objectlist[i], dt);
                fdr.Geometry = ReadGeometry(objectlist[i]);
                if (fdr.Geometry != null)
                    if (fdr.Geometry.GetBoundingBox().Intersects(bbox))
                        if (FilterDelegate == null || FilterDelegate(fdr))
                            dt.AddRow(fdr);
                 */
            }
            ds.Tables.Add(dt);

            CleanInternalCache(objectlist);

        }

        public Collection<uint> GetObjectIDsInView(BoundingBox bbox)
        {
            if (!IsOpen)
                throw (new ApplicationException("An attempt was made to read from a closed datasource"));
            return _tree.Search(bbox);
        }

        public Geometry GetGeometryByID(uint oid)
        {
            if (FilterDelegate != null) //Apply filtering
            {
                FeatureDataRow fdr = GetFeature(oid);
                if (fdr != null)
                    return fdr.Geometry;
                return null;
            }

            if (_useMemoryCache)
            {
                FeatureDataRow fdr;
                _cacheDataTable.TryGetValue(oid, out fdr);
                if (fdr == null)
                {
                    fdr = GetFeature(oid);
                    _cacheDataTable.Add(oid, fdr);
                }

                return fdr.Geometry;
            }

            return ReadGeometry(oid);
        }

        public virtual void ExecuteIntersectionQuery(Geometry geom, FeatureDataSet ds)
        {
            var bbox = geom.GetBoundingBox();

            ExecuteIntersectionQuery(bbox, ds);
            /*
            Collection<uint> objectlist = tree.Search(bbox);

            if (objectlist.Count == 0)
                return;

            for (int j = 0; j < objectlist.Count; j++)
            {
                FeatureDataRow fdr = GetFeature(objectlist[j], dt);

                if (fdr.Geometry != null)
                    if (fdr.Geometry.GetBoundingBox().Intersects(bbox))
                        if (FilterDelegate == null || FilterDelegate(fdr))
                            dt.AddRow(fdr);
            }
            ds.Tables.Add(dt);
             */
        }


        public int GetFeatureCount()
        {
            return _featureCount;
        }

        public FeatureDataRow GetFeature(uint rowId)
        {
            return GetFeature(rowId, DbaseFile.NewTable);
        }

        public BoundingBox GetExtents()
        {
            if (_tree == null)
                throw new ApplicationException(
                    "File hasn't been spatially indexed. Try opening the datasource before retriving extents");
            return _tree.Box;
        }

        public string ConnectionID
        {
            get { return _filename; }
        }

        public virtual int SRID
        {
            get { return _srid; }
            set { _srid = value; }
        }

        #endregion

        private void InitializeShape(string filename, bool fileBasedIndex)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException(String.Format("Could not find file \"{0}\"", filename));
            if (!filename.ToLower().EndsWith(".shp"))
                throw (new Exception("Invalid shapefile filename: " + filename));

            LoadSpatialIndex(fileBasedIndex); //Load spatial index			
        }

        private void ParseHeader()
        {
#if USE_MEMORYMAPPED_FILE
            _fsShapeFile = CheckCreateMemoryMappedStream(_filename, ref _haveRegistredForUsage);
#else
            _fsShapeFile = new FileStream(_filename, FileMode.Open, FileAccess.Read);
#endif
            _brShapeFile = new BinaryReader(_fsShapeFile, Encoding.Unicode);

            _brShapeFile.BaseStream.Seek(0, 0);
            if (_brShapeFile.ReadInt32() != 170328064)
                throw (new ApplicationException("Invalid Shapefile (.shp)"));

            _brShapeFile.BaseStream.Seek(24, 0); //seek to File Length
            _fileSize = 2 * SwapByteOrder(_brShapeFile.ReadInt32());

            _brShapeFile.BaseStream.Seek(32, 0); //seek to ShapeType
            _shapeType = (ShapeType)_brShapeFile.ReadInt32();

            _brShapeFile.BaseStream.Seek(36, 0); //seek to box
            _envelope = new BoundingBox(_brShapeFile.ReadDouble(), _brShapeFile.ReadDouble(), _brShapeFile.ReadDouble(),
                                        _brShapeFile.ReadDouble());

            if (File.Exists(Path.ChangeExtension(_filename, ".shx")))
            {
#if USE_MEMORYMAPPED_FILE
                _fsShapeIndex = CheckCreateMemoryMappedStream(Path.ChangeExtension(_filename, ".shx"), ref _haveRegistredForShxUsage);
#else
                _fsShapeIndex = new FileStream(Path.ChangeExtension(_filename, ".shx"), FileMode.Open, FileAccess.Read);
#endif
                _brShapeIndex = new BinaryReader(_fsShapeIndex, Encoding.Unicode);

                _brShapeIndex.BaseStream.Seek(24, 0); //seek to File Length
                var indexFileSize = SwapByteOrder(_brShapeIndex.ReadInt32()); //Read filelength as big-endian. The length is based on 16bit words
                _featureCount = (2 * indexFileSize - 100) / 8; //Calculate FeatureCount. Each feature takes up 8 bytes. The header is 100 bytes

                _brShapeIndex.Close();
                _fsShapeIndex.Close();
            }
            else
            {
                _brShapeFile.BaseStream.Seek(100, 0); //Skip content length
                long offset = 100; // Start of the data records

                while (offset < _fileSize)
                {
                    ++_featureCount;

                    _brShapeFile.BaseStream.Seek(offset + 4, 0); //Skip content length
                    var dataLength = 2 * SwapByteOrder(_brShapeFile.ReadInt32());

                    if ((offset + dataLength) > _fileSize)
                    {
                        --_featureCount;
                    }

                    offset += dataLength; // Add Record data length
                    offset += 8; //  Plus add the record header size
                }
            }
            _brShapeFile.Close();
            _fsShapeFile.Close();

        }

        private void ParseProjection()
        {
            string projfile = Path.GetDirectoryName(Filename) + "\\" + Path.GetFileNameWithoutExtension(Filename) +
                              ".prj";
            if (File.Exists(projfile))
            {
                try
                {
                    string wkt = File.ReadAllText(projfile);
#if !DotSpatialProjections
                    _coordinateSystem = (ICoordinateSystem)CoordinateSystemWktReader.Parse(wkt);
#else
					_coordinateSystem = new ProjectionInfo();
					_coordinateSystem.ReadEsriString(wkt);
#endif
                    _coordsysReadFromFile = true;
                }
                catch (Exception ex)
                {
                    Trace.TraceWarning("Coordinate system file '" + projfile +
                                       "' found, but could not be parsed. WKT parser returned:" + ex.Message);
                    throw (ex);
                }
            }
        }

        private void PopulateIndexes()
        {
            if (_brShapeIndex != null)
            {
                _brShapeIndex.BaseStream.Seek(100, 0);  //skip the header

                for (int x = 0; x < _featureCount; ++x)
                {
                    _offsetOfRecord[x] = 2 * SwapByteOrder(_brShapeIndex.ReadInt32()); //Read shape data position // ibuffer);
                    _brShapeIndex.BaseStream.Seek(_brShapeIndex.BaseStream.Position + 4, 0); //Skip content length
                }
            }
            else
            {

                var oldPosition = _brShapeFile.BaseStream.Position;

                _brShapeFile.BaseStream.Seek(100, 0); //Skip content length
                long offset = 100; // Start of the data records

                for (int x = 0; x < _featureCount; ++x)
                {
                    _offsetOfRecord[x] = (int)offset;

                    _brShapeFile.BaseStream.Seek(offset + 4, 0); //Skip content length
                    int dataLength = 2 * SwapByteOrder(_brShapeFile.ReadInt32());
                    offset += dataLength; // Add Record data length
                    offset += 8; //  Plus add the record header size
                }

                _brShapeFile.BaseStream.Seek(oldPosition, 0);
            }
        }

        private int SwapByteOrder(int i)
        {
            byte[] buffer = BitConverter.GetBytes(i);
            Array.Reverse(buffer, 0, buffer.Length);
            return BitConverter.ToInt32(buffer, 0);
        }

        private QuadTree CreateSpatialIndexFromFile(string filename)
        {
            if (File.Exists(filename + ".sidx"))
            {
                try
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var tree = QuadTree.FromFile(filename + ".sidx");
                    sw.Stop();
                    Debug.WriteLine(string.Format("Linear creation of QuadTree took {0}ms", sw.ElapsedMilliseconds));
                    return tree;
                }
                catch (QuadTree.ObsoleteFileFormatException)
                {
                    File.Delete(filename + ".sidx");
                    CreateSpatialIndexFromFile(filename);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            switch (SpatialIndexCreationOption)
            {
                case SpatialIndexCreation.Linear:
                    return CreateSpatialIndexLinear(filename);
                default:
                    return CreateSpatialIndexRecursive(filename);
            }
        }

        private QuadTree CreateSpatialIndexLinear(string filename)
        {
            var extent = _envelope;
            var sw = new Stopwatch();
            sw.Start();
            var root = QuadTree.CreateRootNode(extent);
            var h = new Heuristic
            {
                maxdepth = (int)Math.Ceiling(Math.Log(GetFeatureCount(), 2)),
                minerror = 10,
                tartricnt = 5,
                mintricnt = 2
            };

            uint i = 0;
            foreach (var box in GetAllFeatureBoundingBoxes())
            {
                if (!box.IsValid) continue;

                var g = new QuadTree.BoxObjects { Box = box, ID = i };
                root.AddNode(g, h);
                i++;
            }

            sw.Stop();
            Debug.WriteLine(string.Format("Linear creation of QuadTree took {0}ms", sw.ElapsedMilliseconds));

            if (_fileBasedIndexWanted && !string.IsNullOrEmpty(filename))
                root.SaveIndex(filename + ".sidx");
            return root;


        }
        private QuadTree CreateSpatialIndexRecursive(string filename)
        {
            var sw = new Stopwatch();
            sw.Start();

            var objList = new List<QuadTree.BoxObjects>();
            uint i = 0;
            foreach (var box in GetAllFeatureBoundingBoxes())
            {
                if (!box.IsValid) continue;

                var g = new QuadTree.BoxObjects { Box = box, ID = i };
                objList.Add(g);
                i++;
            }

            Heuristic heur;
            heur.maxdepth = (int)Math.Ceiling(Math.Log(GetFeatureCount(), 2));
            heur.minerror = 10;
            heur.tartricnt = 5;
            heur.mintricnt = 2;
            var root = new QuadTree(objList, 0, heur);

            sw.Stop();
            Debug.WriteLine(string.Format("Linear creation of QuadTree took {0}ms", sw.ElapsedMilliseconds));

            if (_fileBasedIndexWanted && !String.IsNullOrEmpty(filename))
                root.SaveIndex(filename + ".sidx");

            return root;
        }


        private void LoadSpatialIndex(bool loadFromFile)
        {
            LoadSpatialIndex(false, loadFromFile);
        }

        public enum SpatialIndexCreation
        {
            Recursive = 0,

            Linear,
        }

        public static SpatialIndexCreation SpatialIndexCreationOption { get; set; }


        private void LoadSpatialIndex(bool forceRebuild, bool loadFromFile)
        {
            if (_tree == null || forceRebuild)
            {
                Func<string, QuadTree> createSpatialIndex;
                if (SpatialIndexCreationOption == SpatialIndexCreation.Recursive)
                    createSpatialIndex = CreateSpatialIndexRecursive;
                else
                    createSpatialIndex = CreateSpatialIndexLinear;

                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Cache[_filename] != null)
                        _tree = (QuadTree)HttpContext.Current.Cache[_filename];
                    else
                    {
                        if (!loadFromFile)
                            _tree = createSpatialIndex(_filename);
                        else
                            _tree = CreateSpatialIndexFromFile(_filename);
                        HttpContext.Current.Cache.Insert(_filename, _tree, null, Cache.NoAbsoluteExpiration,
                                                         TimeSpan.FromDays(1));
                    }
                }
                else if (!loadFromFile)
                    _tree = createSpatialIndex(_filename);
                else
                    _tree = CreateSpatialIndexFromFile(_filename);
            }
        }

        public void RebuildSpatialIndex()
        {
            if (_fileBasedIndex)
            {
                if (File.Exists(_filename + ".sidx"))
                    File.Delete(_filename + ".sidx");
                _tree = CreateSpatialIndexFromFile(_filename);
            }
            else
            {
                switch (SpatialIndexCreationOption)
                {
                    case SpatialIndexCreation.Linear:
                        _tree = CreateSpatialIndexLinear(_filename);
                        break;
                    default:
                        _tree = CreateSpatialIndexRecursive(_filename);
                        break;
                }
            }
            if (HttpContext.Current != null)
                HttpContext.Current.Cache.Insert(_filename, _tree, null, Cache.NoAbsoluteExpiration, TimeSpan.FromDays(1));
        }

        /*
	    private delegate bool RecordDeletedFunction(uint oid);
        private static bool NoRecordDeleted(uint oid)
        {
            return false;
        }
         */

        private IEnumerable<BoundingBox> GetAllFeatureBoundingBoxes()
        {

            /*
		    RecordDeletedFunction recDel = dbaseFile != null
		                                       ? (RecordDeletedFunction) dbaseFile.RecordDeleted
		                                       : NoRecordDeleted;
             */
            if (_shapeType == ShapeType.Point)
            {
                for (int a = 0; a < _featureCount; ++a)
                {

                    _fsShapeFile.Seek(_offsetOfRecord[a] + 8, 0); //skip record number and content length
                    if ((ShapeType)_brShapeFile.ReadInt32() != ShapeType.Null)
                    {
                        double x = _brShapeFile.ReadDouble();
                        double y = _brShapeFile.ReadDouble();
                        yield return new BoundingBox(x, y, x, y);
                    }
                }
            }
            else
            {
                for (int a = 0; a < _featureCount; ++a)
                {
                    _fsShapeFile.Seek(_offsetOfRecord[a] + 8, 0); //skip record number and content length
                    if ((ShapeType)_brShapeFile.ReadInt32() != ShapeType.Null)
                        yield return new BoundingBox(_brShapeFile.ReadDouble(), _brShapeFile.ReadDouble(),
                                                     _brShapeFile.ReadDouble(), _brShapeFile.ReadDouble());
                }
            }
        }

        private Geometry ReadGeometry(uint oid)
        {
            _brShapeFile.BaseStream.Seek(_offsetOfRecord[oid] + 8, 0); //Skip record number and content length
            ShapeType type = (ShapeType)_brShapeFile.ReadInt32(); //Shape type
            if (type == ShapeType.Null)
                return null;
            if (_shapeType == ShapeType.Point || _shapeType == ShapeType.PointM || _shapeType == ShapeType.PointZ)
            {
                Point tempFeature = new Point(_brShapeFile.ReadDouble(), _brShapeFile.ReadDouble());
                tempFeature.GeometryId = oid;
                return tempFeature;
            }
            else if (_shapeType == ShapeType.Multipoint || _shapeType == ShapeType.MultiPointM ||
                     _shapeType == ShapeType.MultiPointZ)
            {
                _brShapeFile.BaseStream.Seek(32 + _brShapeFile.BaseStream.Position, 0); //skip min/max box
                MultiPoint feature = new MultiPoint();
                int nPoints = _brShapeFile.ReadInt32(); // get the number of points
                if (nPoints == 0)
                    return null;
                for (int i = 0; i < nPoints; i++)
                    feature.Points.Add(new Point(_brShapeFile.ReadDouble(), _brShapeFile.ReadDouble()));
                feature.GeometryId = oid;
                return feature;
            }
            else if (_shapeType == ShapeType.PolyLine || _shapeType == ShapeType.Polygon ||
                     _shapeType == ShapeType.PolyLineM || _shapeType == ShapeType.PolygonM ||
                     _shapeType == ShapeType.PolyLineZ || _shapeType == ShapeType.PolygonZ)
            {
                _brShapeFile.BaseStream.Seek(32 + _brShapeFile.BaseStream.Position, 0); //skip min/max box

                int nParts = _brShapeFile.ReadInt32(); // get number of parts (segments)
                if (nParts == 0 || nParts < 0)
                    return null;
                int nPoints = _brShapeFile.ReadInt32(); // get number of points

                int[] segments = new int[nParts + 1];
                for (int b = 0; b < nParts; b++)
                    segments[b] = _brShapeFile.ReadInt32();
                segments[nParts] = nPoints;

                if ((int)_shapeType % 10 == 3)
                {
                    MultiLineString mline = new MultiLineString();
                    for (int LineID = 0; LineID < nParts; LineID++)
                    {
                        LineString line = new LineString();
                        for (int i = segments[LineID]; i < segments[LineID + 1]; i++)
                            line.Vertices.Add(new Point(_brShapeFile.ReadDouble(), _brShapeFile.ReadDouble()));
                        mline.LineStrings.Add(line);
                    }
                    if (mline.LineStrings.Count == 1)
                    {
                        mline[0].GeometryId = oid;
                        return mline[0];
                    }
                    mline.GeometryId = oid;
                    return mline;
                }
                else //(_ShapeType == ShapeType.Polygon etc...)
                {
                    List<LinearRing> rings = new List<LinearRing>();
                    for (int RingID = 0; RingID < nParts; RingID++)
                    {
                        LinearRing ring = new LinearRing();
                        for (int i = segments[RingID]; i < segments[RingID + 1]; i++)
                            ring.Vertices.Add(new Point(_brShapeFile.ReadDouble(), _brShapeFile.ReadDouble()));
                        rings.Add(ring);
                    }
                    bool[] IsCounterClockWise = new bool[rings.Count];
                    int PolygonCount = 0;
                    for (int i = 0; i < rings.Count; i++)
                    {
                        IsCounterClockWise[i] = rings[i].IsCCW();
                        if (!IsCounterClockWise[i])
                            PolygonCount++;
                    }
                    if (PolygonCount == 1) //We only have one polygon
                    {
                        Polygon poly = new Polygon();
                        poly.ExteriorRing = rings[0];
                        if (rings.Count > 1)
                            for (int i = 1; i < rings.Count; i++)
                                poly.InteriorRings.Add(rings[i]);
                        poly.GeometryId = oid;
                        return poly;
                    }
                    else
                    {
                        MultiPolygon mpoly = new MultiPolygon();
                        Polygon poly = new Polygon();
                        poly.ExteriorRing = rings[0];
                        for (int i = 1; i < rings.Count; i++)
                        {
                            if (!IsCounterClockWise[i])
                            {
                                mpoly.Polygons.Add(poly);
                                poly = new Polygon(rings[i]);
                            }
                            else
                                poly.InteriorRings.Add(rings[i]);
                        }
                        mpoly.Polygons.Add(poly);
                        mpoly.GeometryId = oid;
                        return mpoly;
                    }
                }
            }
            else
                throw (new ApplicationException("Shapefile type " + _shapeType.ToString() + " not supported"));
        }

        public FeatureDataRow GetFeature(uint rowId, FeatureDataTable dt)
        {
            Debug.Assert(dt != null);
            if (DbaseFile != null)
            {
                if (_useMemoryCache)
                {
                    FeatureDataRow dr2;
                    _cacheDataTable.TryGetValue(rowId, out dr2);
                    if (dr2 == null)
                    {
                        dr2 = DbaseFile.GetFeature(rowId, dt);
                        dr2.Geometry = ReadGeometry(rowId);
                        _cacheDataTable.Add(rowId, dr2);
                    }

                    FeatureDataRow drNew = dt.NewRow();
                    for (int i = 0; i < dr2.Table.Columns.Count; i++)
                    {
                        drNew[i] = dr2[i];
                    }
                    drNew.Geometry = dr2.Geometry;
                    return drNew;
                }

                FeatureDataRow dr = DbaseFile.GetFeature(rowId, dt);
                dr.Geometry = ReadGeometry(rowId);
                if (FilterDelegate == null || FilterDelegate(dr))
                    return dr;

                return null;
            }

            throw (new ApplicationException(
                "An attempt was made to read DBase data from a shapefile without a valid .DBF file"));
        }
    }
}
