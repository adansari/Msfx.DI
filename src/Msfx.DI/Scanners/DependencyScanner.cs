using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Scanners
{
    public abstract class DependencyScanner
    {
        protected Assembly _targetAssembly;

        protected DependencyScanner(Assembly targetAssembly)
        {
            this._targetAssembly = targetAssembly;
        }

        public abstract IEnumerable<Type> Scan();

        public static DependencyScanner GetDependencyScanner(DependencyScanTarget dependencyScanTarget,Assembly assembly,string currentNamespace)
        {
            switch (dependencyScanTarget)
            {
                case DependencyScanTarget.CurrentNamespace:
                    return new NamespaceScanner(assembly, currentNamespace);
                case DependencyScanTarget.CurrentNamespaceRecursive:
                    return new NamespaceRecursiveScanner(assembly, currentNamespace);
                case DependencyScanTarget.Assembly:
                    return new AssemblyScanner(assembly);
                default:
                    return null;
            }
        }
    }

    public enum DependencyScanTarget
    {
        CurrentNamespace,
        CurrentNamespaceRecursive,
        Assembly,
        None
    }
}
