using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Scanners
{
    public class NamespaceScanner : DependencyScanner
    {
        private string _namespace;
        
        public NamespaceScanner(Assembly targetAssembly,string namespaceToScan) : base(targetAssembly)
        {
            this._namespace = namespaceToScan;
        }

        public override IEnumerable<Type> Scan()
        {
            return from t in this._targetAssembly.GetTypes()
                   where (t.IsClass || t.IsInterface) && t.Namespace == this._namespace
                   select t;
        }
    }
}
