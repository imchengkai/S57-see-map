

using System;
using System.Collections;

namespace EasyMap.Layers
{
    public class LayerCollection : System.ComponentModel.BindingList<ILayer>
    {

        public virtual ILayer this[string layerName]
        {
            get { return GetLayerByName(layerName); }
            set
            {
                for (int i = 0; i < Count; i++)
                {
                    int comparison = String.Compare(this[i].LayerName,
                                                    layerName, StringComparison.CurrentCultureIgnoreCase);

                    if (comparison == 0)
                    {
                        this[i] = value;
                        return;
                    }
                }

                Add(value);
            }
        }


        public new void Insert(int index, ILayer layer)
        {
            if (index > Count || index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index not in range");
            }


            base.InsertItem(0, layer);

        }

        protected override void OnAddingNew(System.ComponentModel.AddingNewEventArgs e)
        {
            ILayer newLayer = (e.NewObject as ILayer);
            if (newLayer == null) throw new ArgumentNullException("value", "The passed argument is null or not an ILayer");

            foreach (ILayer layer in this)
            {
                int comparison = String.Compare(layer.LayerName,
                                                newLayer.LayerName, StringComparison.CurrentCultureIgnoreCase);

                if (comparison == 0) throw new DuplicateLayerException(newLayer.LayerName);
            }

            base.OnAddingNew(new System.ComponentModel.AddingNewEventArgs(newLayer));
        }

        private ILayer GetLayerByName(string layerName)
        {
            foreach (ILayer layer in this)
            {
                int comparison = String.Compare(layer.LayerName,
                                                layerName, StringComparison.CurrentCultureIgnoreCase);

                if (comparison == 0) return layer;
            }

            return null;
        }

    }
}
