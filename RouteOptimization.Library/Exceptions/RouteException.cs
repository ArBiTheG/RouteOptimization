using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Exceptions
{
    public class RouteException : Exception
    {
        public RouteException()
        {
        }

        public RouteException(string? message) : base(message)
        {
        }

        public RouteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RouteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
