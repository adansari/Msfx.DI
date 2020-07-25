using Msfx.DI.LifeTimeManagers;
using System;
using System.Collections.Generic;

namespace Msfx.DI
{
    public class DependencyMap : IDependencyMap
    {
        public string SourceDependencyId { get; }
        public bool IsAbstractOrInterface { get; } = false;
        public IDependencyHolder PrimaryDependencyHolder { get; set; }
        public List<IDependencyHolder> SecondaryDependencyHolder { get;}
        public DependencyMap(Type sourceDependencyType)
        {
            this.SourceDependencyId = sourceDependencyType.GetDependencyId();

            this.SecondaryDependencyHolder = new List<IDependencyHolder>();

            if (sourceDependencyType.IsAbstract || sourceDependencyType.IsInterface)
            {
                this.IsAbstractOrInterface = true;
            }
            else
            {
                this.PrimaryDependencyHolder = new DependencyHolder(sourceDependencyType);
            }
        }
        public DependencyMap(Type sourceDependencyType, InstanceType instanceType)
        {
            this.SourceDependencyId = sourceDependencyType.GetDependencyId();

            this.SecondaryDependencyHolder = new List<IDependencyHolder>();

            if (sourceDependencyType.IsAbstract || sourceDependencyType.IsInterface)
            {
                this.IsAbstractOrInterface = true;
            }
            else
            {
                this.PrimaryDependencyHolder = new DependencyHolder(sourceDependencyType,instanceType);
            }
        }

        public IDependencyHolder GetSecondaryDependencyHolder(string depedencyId)
        {
            return this.SecondaryDependencyHolder.Find(d => d.DependencyId == depedencyId);
        }
    }
}
