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
    public class NonInjectableTypeException : DIException
    {
        public NonInjectableTypeException() { }
        public NonInjectableTypeException(string message) : base(message) { }
        public NonInjectableTypeException(string message, Exception inner) : base(message, inner) { }
        protected NonInjectableTypeException(SerializationInfo info,StreamingContext context) : base(info, context) { }
    }
}
