using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class CanBeInjectedForAttribute : Attribute
    {
        readonly Type _sourceType;

        // This is a positional argument
        public CanBeInjectedForAttribute(Type sourceType)
        {
            this._sourceType = sourceType;
        }

        public Type SourceType
        {
            get { return this._sourceType; }
        }
    }
}
