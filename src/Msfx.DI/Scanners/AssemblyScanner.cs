using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Scanners
{
    public class AssemblyScanner : DependencyScanner
    {
        public AssemblyScanner(Assembly targetAssembly) : base(targetAssembly) { }

        public override IEnumerable<Type> Scan()
        {
            return from t in this._targetAssembly.GetTypes()
                   where t.IsClass || t.IsInterface
                   select t;
        }
    }
}
