﻿using Msfx.DI.Attributes;
using Msfx.DI.Containers;
using Msfx.DI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors
{
    public class ConstructorAutoInjector : AutoInjector
    {
        public ConstructorAutoInjector(IDIContainer container, Type type) :base(container,type){ }

        public ConstructorAutoInjector(AutoInjector successor, IDIContainer container, Type type) : base(successor, container, type) { }

        public override void Inject(object instance)
        {
            //Constructor Injection

            var ctorsToAutoInject = this._type.GetMembers().Where(ctor => Attribute.IsDefined(ctor, typeof(AutoInjectAttribute)) && ctor.MemberType == MemberTypes.Constructor);

            foreach (var ctor in ctorsToAutoInject)
            {
                ConstructorInfo ctorInfo = (ConstructorInfo)ctor;
                object[] paramValues = new object[ctorInfo.GetParameters().Length];
                int index = 0;

                foreach (ParameterInfo parameter in ctorInfo.GetParameters())
                {
                    string dependencyId = GetParamPreferredDependency(parameter) ?? parameter.ParameterType.GetDependencyId();

                    if (this.Container.ContainsDependency(dependencyId))
                    {
                        IDependencyHolder primaryDepHolder = this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder;

                        if (primaryDepHolder == null) throw new PrimaryOrPreferredTargetDependencyNotFound("Source dependency: " + dependencyId);

                        object memberValue = primaryDepHolder.GetInstance(null);

                        paramValues[index] = memberValue;

                        index++;
                    }
                    else
                    {
                        string errMsg = string.Format("{0} dependency not found or is not attributed as Injectable while auto injecting the constructor of dependency:{1}", dependencyId,this._type.GetDependencyId());
                        throw new NonInjectableTypeException();
                    }
                }

                var o = ctorInfo.Invoke(instance, paramValues);
            }

            this.InjectNext(instance);
        }
    }
}
