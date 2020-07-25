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
    public class InjectionAmbiguityException : DIException
    {
        public InjectionAmbiguityException() { }
        public InjectionAmbiguityException(string message) : base(message) { }
        public InjectionAmbiguityException(string message, Exception inner) : base(message, inner) { }
        protected InjectionAmbiguityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
