

using System.Drawing;
#if !DotSpatialProjections
using ProjNet.CoordinateSystems.Transformations;
#else
using DotSpatial.Projections;
#endif
using EasyMap.Geometries;
using EasyMap.Styles;
using EasyMap.Rendering;

namespace EasyMap.Layers
{
    public abstract class Layer : ILayer
    {
        private decimal _ID = 0;
        private int _SortNo = 0;
        private bool _NeedSave = true;
        private bool _AllowEdit = false;

        public bool AllowEdit
        {
            get { return _AllowEdit; }
            set { _AllowEdit = value; }
        }

        public bool NeedSave
        {
            get { return _NeedSave; }
            set { _NeedSave = value; }
        }

        public int SortNo
        {
            get { return _SortNo; }
            set { _SortNo = value; }
        }
        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private EasyMap.Layers.VectorLayer.LayerType _Type = EasyMap.Layers.VectorLayer.LayerType.BaseLayer;

        public EasyMap.Layers.VectorLayer.LayerType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        #region Events

        #region Delegates

        public delegate void LayerRenderedEventHandler(Layer layer, Graphics g);

        #endregion

        public event LayerRenderedEventHandler LayerRendered;

        #endregion

        private ICoordinateTransformation _coordinateTransform = null;
        private ICoordinateTransformation _reverseCoordinateTransform = null;

        private string _layerName;
        private Style _style;
        private int _srid = -1;

        public Layer(Style style)
        {
            _style = style;
        }

        protected Layer() //Style style)
        {
            _style = new Style();
        }

#if !DotSpatialProjections
#else
#endif
        public virtual ICoordinateTransformation CoordinateTransformation
        {
            get { return _coordinateTransform; }
            set { _coordinateTransform = value; }
        }

#if !DotSpatialProjections
        public virtual ICoordinateTransformation ReverseCoordinateTransformation
        {
            get { return _reverseCoordinateTransform; }
            set { _reverseCoordinateTransform = value; }
        }
#endif

        #region ILayer Members

        public string LayerName
        {
            get { return _layerName; }
            set { _layerName = value; }
        }

        public virtual int SRID
        {
            get { return _srid; }
            set { _srid = value; }
        }



        public virtual void Render(Graphics g, Map map,RenderType rendertype)
        {
            if (LayerRendered != null) LayerRendered(this, g); //Fire event
        }

        public abstract BoundingBox Envelope { get; }

        #endregion

        #region Properties

        /*
        private bool _Enabled = true;
        private double _MaxVisible = double.MaxValue;
        private double _MinVisible = 0;
        */
        public double MinVisible
        {
            get
            {
                return _style.MinVisible; // return _MinVisible;
            }
            set
            {
                _style.MinVisible = value; // _MinVisible = value; 
            }
        }

        public double MaxVisible
        {
            get
            {
                return _style.MaxVisible;
            }
            set
            {
                _style.MaxVisible = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return _style.Enabled;
            }
            set
            {
                _style.Enabled = value;
            }
        }

        public virtual Style Style
        {
            get { return _style; }
            set { _style = value; }
        }

        #endregion

        public override string ToString()
        {
            return LayerName;
        }
    }
}
