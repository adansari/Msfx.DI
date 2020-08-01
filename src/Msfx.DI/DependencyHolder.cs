using System;
using Msfx.DI.Containers;
using Msfx.DI.LifetimeManagers;

namespace Msfx.DI
{
    public class DependencyHolder : IDependencyHolder
    {
        public DependencyHolder(IDIContainer container,Type dependencyType)
        {
            this.Container = container;
            this.DependencyType = dependencyType;
            this.InstanceType = InstanceType.Static;
            this.LifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(this.Container, DependencyType, this.InstanceType);
        }

        public DependencyHolder(IDIContainer container,Type dependencyType, InstanceType instanceType)
        {
            this.Container = container;
            this.DependencyType = dependencyType;
            this.InstanceType = instanceType;
            this.LifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(this.Container, DependencyType, this.InstanceType);
        }

        public IDIContainer Container { get; }

        public virtual string DependencyId
        {
            get
            {
                return DependencyType.GetDependencyId();
            }
        }
        public InstanceType InstanceType { get; } 

        public virtual InstanceLifetimeManager LifetimeManager { get; }

        public Type DependencyType { get; }

        public object GetInstance(object[] args)
        {
            return this.LifetimeManager.CreateOrGetInstance(args);
        }
    }
}
