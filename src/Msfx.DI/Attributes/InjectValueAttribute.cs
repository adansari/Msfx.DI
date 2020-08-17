using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Attributes
{
    [System.AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field| AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]

    public sealed class InjectValueAttribute : Attribute
    {
        private readonly object[] _values;
        public InjectValueAttribute(params object[] values)
        {
            this._values = values;
        }

        public object[] Values
        {
            get { return this._values; }
        }
    }
}
