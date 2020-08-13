using Msfx.DI.Attributes;
using Msfx.DI.Containers;
using Msfx.DI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Registrars
{
    public class CanBeInjectedDependencyRegistrar : DependencyRegistrar
    {
        public CanBeInjectedDependencyRegistrar(IDIContainer container) : base(container)
        { }
        public CanBeInjectedDependencyRegistrar(DependencyRegistrar successor, IDIContainer container) : base(successor, container)
        { }
        public override void Register(IEnumerable<Type> types)
        {
            foreach (Type t in types)
            {
                if (Attribute.IsDefined(t, typeof(CanBeInjectedForAttribute)))
                {
                    Attribute[] canBeInjectedForAttributes = Attribute.GetCustomAttributes(t, typeof(CanBeInjectedForAttribute));

                    foreach (Attribute eachAttribute in canBeInjectedForAttributes)
                    {
                        CanBeInjectedForAttribute canBeInjectedFor = eachAttribute as CanBeInjectedForAttribute;

                        string sourceDependencyId = canBeInjectedFor.SourceType.GetDependencyId();
                        string targetDependencyId = t.GetDependencyId();

                        if (sourceDependencyId == targetDependencyId)
                            continue;

                        if (!this.Container.ContainsDependency(sourceDependencyId))
                            throw new NonInjectableTypeException(sourceDependencyId + " dependency is not attributed as Injectable");

                        if (!this.Container.ContainsDependency(targetDependencyId))
                            throw new NonInjectableTypeException(targetDependencyId + " dependency is not attributed as Injectable");

                        IDependencyMap sourceDependencyMap = this.Container.GetDependencyMap(sourceDependencyId);

                        sourceDependencyMap.SecondaryDependencyHolders.Add(this.Container.GetDependencyMap(targetDependencyId).PrimaryDependencyHolder);
                    }
                }
            }

            this.RegisterNext(types);
        }
    }
}
