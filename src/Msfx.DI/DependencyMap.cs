using Msfx.DI.Containers;
using Msfx.DI.LifetimeManagers;
using System;
using System.Collections.Generic;

namespace Msfx.DI
{
    public class DependencyMap : IDependencyMap
    {
        public DependencyMap(IDIContainer container, Type sourceDependencyType)
        {
            this.Container = container;

            this.SourceDependencyId = sourceDependencyType.GetDependencyId();

            this.SecondaryDependencyHolder = new List<IDependencyHolder>();

            if (sourceDependencyType.IsAbstract || sourceDependencyType.IsInterface)
            {
                this.IsAbstractOrInterface = true;
            }
            else
            {
                this.PrimaryDependencyHolder = new DependencyHolder(this.Container,sourceDependencyType);
            }
        }
        public DependencyMap(IDIContainer container,Type sourceDependencyType, InstanceType instanceType)
        {
            this.Container = container;

            this.SourceDependencyId = sourceDependencyType.GetDependencyId();

            this.SecondaryDependencyHolder = new List<IDependencyHolder>();

            if (sourceDependencyType.IsAbstract || sourceDependencyType.IsInterface)
            {
                this.IsAbstractOrInterface = true;
            }
            else
            {
                this.PrimaryDependencyHolder = new DependencyHolder(this.Container, sourceDependencyType, instanceType);
            }
        }
        public IDIContainer Container{get;}
        public string SourceDependencyId { get; }
        public bool IsAbstractOrInterface { get; } = false;
        public IDependencyHolder PrimaryDependencyHolder { get; set; }
        public List<IDependencyHolder> SecondaryDependencyHolder { get;}
        public IDependencyHolder GetSecondaryDependencyHolder(string depedencyId)
        {
            return this.SecondaryDependencyHolder.Find(d => d.DependencyId == depedencyId);
        }
    }
}
