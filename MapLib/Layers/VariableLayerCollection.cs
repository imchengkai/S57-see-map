using System;
using System.Timers;

namespace EasyMap.Layers
{
    public enum LayerCollectionType
    {
        Static,

        Variable,

        Background,
    }
    public delegate void VariableLayerCollectionRequeryHandler(object sender, EventArgs e);

    public class VariableLayerCollection : LayerCollection
    {
        private readonly LayerCollection _staticLayers;
        private static Timer _timer = null;

        private static bool touchTest = false;
        public static void TouchTimer()
        {
            if (touchTest == true)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(OnRequery));
                _timer.Start();
            }
            else
            {
                touchTest = true;
            }
        }

        public static event VariableLayerCollectionRequeryHandler VariableLayerCollectionRequery;

        public VariableLayerCollection(LayerCollection staticLayers)
        {
            _staticLayers = staticLayers;
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = 500;
                _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            }
        }

        static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            touchTest = false;
            OnRequery(null);
            if (touchTest == false)
            {
                _timer.Stop();
                touchTest = true;
            }
        }
        protected override void OnAddingNew(System.ComponentModel.AddingNewEventArgs e)
        {
            ILayer newLayer = (e.NewObject as ILayer);
            if (newLayer == null) throw new ArgumentNullException("value", "The passed argument is null or not an ILayer");

            TestLayerPresent(_staticLayers, newLayer);

            base.OnAddingNew(e);
        }


        private static void TestLayerPresent(LayerCollection layers, ILayer newLayer)
        {
            foreach (ILayer layer in layers)
            {
                int comparison = String.Compare(layer.LayerName,
                                                newLayer.LayerName, StringComparison.CurrentCultureIgnoreCase);

                if (comparison == 0) throw new DuplicateLayerException(newLayer.LayerName);
            }

        }

        private static void OnRequery(object obj)
        {
            if (Pause) return;
            if (VariableLayerCollectionRequery != null)
                VariableLayerCollectionRequery(null, EventArgs.Empty);
        }

        public static double Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        private static bool _pause;
        public static bool Pause
        {
            get { return _pause; }
            set { _pause = value; }
        }
    }
}
