using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Attributes
{
    [System.AttributeUsage(AttributeTargets.Constructor|AttributeTargets.Property|AttributeTargets.Method|AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class AutoInjectAttribute : Attribute{}
}
