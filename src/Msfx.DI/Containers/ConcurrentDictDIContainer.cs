using Msfx.DI.Exceptions;
using Msfx.DI.LifeTimeManagers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Containers
{
    public class ConcurrentDictDIContainer : IDIContainer
    {
        public ConcurrentDictionary<string, IDependencyMap> _container;

        public IDependencyMap this[string dependencyId] => GetDependencyMap(dependencyId);

        public virtual ConcurrentDictionary<string, IDependencyMap> Container { get { return this._container; } }

        public ConcurrentDictDIContainer()
        {
            this._container = new ConcurrentDictionary<string, IDependencyMap>();
        }

        public bool ContainsDependency(string dependencyId)
        {
            return this.Container.ContainsKey(dependencyId);
        }

        public virtual IDependencyMap GetDependencyMap(string dependencyId)
        {
            IDependencyMap dependencyMap;

            this.Container.TryGetValue(dependencyId, out dependencyMap);

            return dependencyMap;
        }

        public bool Register(string dependencyId, IDependencyMap dependencyMap)
        {
            return this.Container.TryAdd(dependencyId, dependencyMap);
        }

        public bool Register(string dependencyId, Type type, InstanceType instanceType)
        {
            return this.Container.TryAdd(dependencyId, new DependencyMap(type, instanceType));
        }
    }
}
