using System;
using Msfx.DI.LifeTimeManagers;

namespace Msfx.DI
{
    public class DependencyHolder : IDependencyHolder
    {
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

        public DependencyHolder(Type dependencyType)
        {
            this.DependencyType = dependencyType;
            this.InstanceType = InstanceType.Static;
            this.LifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(DependencyType,this.InstanceType);
        }

        public DependencyHolder(Type dependencyType,InstanceType instanceType)
        {
            this.DependencyType = dependencyType;
            this.InstanceType = instanceType;
            this.LifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(DependencyType, this.InstanceType);
        }

        public object GetInstance(object[] args)
        {
            return this.LifetimeManager.CreateOrGetInstance(args);
        }
    }
}
