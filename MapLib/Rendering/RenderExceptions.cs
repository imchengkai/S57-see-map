

using System;

namespace EasyMap.Rendering.Exceptions
{
    public class RenderException : Exception
    {
        public RenderException()
        {
        }

        public RenderException(string message)
            : base(message)
        {
        }

        public RenderException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
