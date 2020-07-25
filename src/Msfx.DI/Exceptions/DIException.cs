using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class DIException : Exception
    {
        public DIException() { }
        public DIException(string message) : base(message) { }
        public DIException(string message, Exception inner) : base(message, inner) { }
        protected DIException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
