using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Attributes
{
    [System.AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field| AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]

    public sealed class PreferredTypeAttribute : Attribute
    {
        private readonly Type _type;
        public PreferredTypeAttribute(Type type)
        {
            this._type = type;
        }

        public Type Type
        {
            get { return this._type; }
        }
    }
}
