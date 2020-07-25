using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class InjectForTypeAttribute : Attribute
    {
        private readonly Type _type;

        public InjectForTypeAttribute(Type type)
        {
            this._type = type;
        }

        public Type Type
        {
            get { return this._type; }
        }
    }
}
