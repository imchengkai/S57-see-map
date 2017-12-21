using System;
using System.Runtime.Serialization;

namespace EasyMap.Layers
{
    [Serializable]
    public class DuplicateLayerException : InvalidOperationException
    {
        private readonly string _duplicateLayerName;

        public DuplicateLayerException(string duplicateLayerName)
            : this(duplicateLayerName, null)
        {
        }

        public DuplicateLayerException(string duplicateLayerName, string message)
            : this(duplicateLayerName, message, null)
        {
        }

        public DuplicateLayerException(string duplicateLayerName, string message, Exception inner)
            : base(message, inner)
        {
            _duplicateLayerName = duplicateLayerName;
        }

        protected DuplicateLayerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _duplicateLayerName = info.GetString("_duplicateLayerName");
        }

        public string DuplicateLayerName
        {
            get { return _duplicateLayerName; }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("_duplicateLayerName", _duplicateLayerName);
        }
    }
}
