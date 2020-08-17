using Msfx.DI.LifetimeManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class InjectableAttribute : Attribute
    {
        public InstanceType InstanceType { get; set; } = InstanceType.Static;
    }
}
