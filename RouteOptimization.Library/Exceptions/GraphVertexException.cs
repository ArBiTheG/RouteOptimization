using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Exceptions
{
    public class GraphVertexException : Exception
    {
        public GraphVertexException()
        {
        }

        public GraphVertexException(string? message) : base(message)
        {
        }

        public GraphVertexException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GraphVertexException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
