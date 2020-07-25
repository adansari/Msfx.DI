using Msfx.DI.Factories;
using Msfx.DI.LifeTimeManagers;
using System;

namespace Msfx.DI
{
    public interface IDependencyHolder
    {
        string DependencyId { get; }
        InstanceType InstanceType { get; }
        Type DependencyType { get; }
        InstanceLifetimeManager LifetimeManager { get; }
        object GetInstance(object[] args);
    }
}