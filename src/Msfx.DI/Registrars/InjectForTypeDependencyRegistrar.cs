﻿using Msfx.DI.Attributes;
using Msfx.DI.Containers;
using Msfx.DI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Registrars
{
    public class InjectForTypeDependencyRegistrar : DependencyRegistrar
    {
        public InjectForTypeDependencyRegistrar(IDIContainer container) : base(container)
        { }
        public InjectForTypeDependencyRegistrar(DependencyRegistrar successor, IDIContainer container) : base(successor, container)
        { }
        public override void Register(IEnumerable<Type> types)
        {
            foreach (Type t in types)
            {
                if (Attribute.IsDefined(t, typeof(InjectForTypeAttribute)))
                {
                    Attribute[] injectForTypeAttributes = Attribute.GetCustomAttributes(t, typeof(InjectForTypeAttribute));

                    foreach (Attribute eachAttribute in injectForTypeAttributes)
                    {
                        InjectForTypeAttribute injectForType = eachAttribute as InjectForTypeAttribute;

                        string sourceDependencyId = injectForType.Type.GetDependencyId();
                        string targetDependencyId = t.GetDependencyId();

                        if (sourceDependencyId == targetDependencyId) continue;

                        if (!this.Container.ContainsDependency(sourceDependencyId))
                            throw new NonInjectableTypeException(sourceDependencyId + " dependency is not attributed as Injectable");

                        if (!this.Container.ContainsDependency(targetDependencyId))
                            throw new NonInjectableTypeException(targetDependencyId + " dependency is not attributed as Injectable");

                        IDependencyMap sourceDependencyMap = this.Container.GetDependencyMap(sourceDependencyId);

                        if (sourceDependencyMap.PrimaryDependencyHolder == null)
                        {
                            sourceDependencyMap.PrimaryDependencyHolder = this.Container.GetDependencyMap(targetDependencyId).PrimaryDependencyHolder;
                        }
                        else
                        {
                            throw new InjectionAmbiguityException("More than one types(" + sourceDependencyMap.PrimaryDependencyHolder.DependencyId + ", " + this.Container[targetDependencyId].PrimaryDependencyHolder.DependencyId + ") can not be set as to be injected for " + sourceDependencyId);
                        }

                        sourceDependencyMap.SecondaryDependencyHolders.Add(this.Container.GetDependencyMap(targetDependencyId).PrimaryDependencyHolder);
                    }
                }
            }

            this.RegisterNext(types);
        }
    }
}
