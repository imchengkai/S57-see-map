using EasyMap.Data.Providers;
using EasyMap.Geometries;
using EasyMap.Rendering.Symbolizer;

namespace EasyMap.Layers.Symbolizer
{
    /// <summary>
    /// A vector layer class that can symbolize lineal geometries
    /// </summary>
    public class LinealVectorLayer : BaseVectorLayer<ILineal> {
        
        /// <summary>
        /// Creates an instance of this class
        /// </summary>
        /// <param name="layerName">The layer's name</param>
        public LinealVectorLayer(string layerName) : this(layerName, null)
        {
        }

        /// <summary>
        /// Creates an instance of this class
        /// </summary>
        /// <param name="layerName">The layer's name</param>
        /// <param name="dataSource">The layer's datasource</param>
        public LinealVectorLayer(string layerName, IProvider dataSource)
            : this(layerName, dataSource, new BasicLineSymbolizer())
        {
        }

        /// <summary>
        /// Creates an instance of this class
        /// </summary>
        /// <param name="layerName">The layer's name</param>
        /// <param name="dataSource">The layer's datasource</param>
        /// <param name="symbolizer">The layer's symbolizer</param>
        public LinealVectorLayer(string layerName, IProvider dataSource, ISymbolizer<ILineal> symbolizer)
            : base(layerName, dataSource, symbolizer)
        {
        }
    }
}