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
    public class PrimaryOrPreferredTargetDependencyNotFound : DIException
    {
        public PrimaryOrPreferredTargetDependencyNotFound() { }
        public PrimaryOrPreferredTargetDependencyNotFound(string message) : base(message) { }
        public PrimaryOrPreferredTargetDependencyNotFound(string message, Exception inner) : base(message, inner) { }
        protected PrimaryOrPreferredTargetDependencyNotFound(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
