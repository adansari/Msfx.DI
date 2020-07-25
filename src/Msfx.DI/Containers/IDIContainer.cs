using Msfx.DI.LifeTimeManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Containers
{
    public interface IDIContainer
    {
        IDependencyMap this[string dependencyId] {get;}
        bool ContainsDependency(string dependencyId);

        IDependencyMap GetDependencyMap(string dependencyId);

        bool Register(string dependencyId, IDependencyMap dependencyMap);

        bool Register(string dependencyId, Type type, InstanceType instanceType);
    }
}
