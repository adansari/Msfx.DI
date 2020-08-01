using Msfx.DI.Exceptions;
using Msfx.DI.LifetimeManagers;
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

        public List<IDependencyMap> SearchDependency(string typeName)
        {
            var searchedKeys = this.Container.Keys.Where(k => k.EndsWith("." + typeName)).ToList();

            List<IDependencyMap> dependencyMaps = new List<IDependencyMap>();

            foreach(string searchedKey in searchedKeys)
            {
                dependencyMaps.Add(this.GetDependencyMap(searchedKey));
            }

            return dependencyMaps;
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
            return this.Container.TryAdd(dependencyId, new DependencyMap(this,type, instanceType));
        }
    }
}
