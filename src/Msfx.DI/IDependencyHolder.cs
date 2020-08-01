using Msfx.DI.Containers;
using Msfx.DI.Factories;
using Msfx.DI.LifetimeManagers;
using System;

namespace Msfx.DI
{
    public interface IDependencyHolder
    {
        IDIContainer Container { get; }
        string DependencyId { get; }
        InstanceType InstanceType { get; }
        Type DependencyType { get; }
        InstanceLifetimeManager LifetimeManager { get; }
        object GetInstance(object[] args);
    }
}