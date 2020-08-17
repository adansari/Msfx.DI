using Msfx.DI.Attributes;
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
    public class MethodAutoInjector : MemberAutoInjector
    {
        public MethodAutoInjector(IDIContainer container, Type type) :base(container,type){ }

        public MethodAutoInjector(MemberAutoInjector successor, IDIContainer container, Type type) : base(successor,container, type) { }

        public override void Inject(object instance)
        {
            //Method Injection

            var methodToAutoInject = this._type.GetMembers().Where(ctor => Attribute.IsDefined(ctor, typeof(AutoInjectAttribute)) && ctor.MemberType == MemberTypes.Method);

            foreach (var method in methodToAutoInject)
            {
                MethodInfo methodInfo = (MethodInfo)method;
                object[] paramValues = new object[methodInfo.GetParameters().Length];
                int index = 0;

                foreach (ParameterInfo paramInfo in methodInfo.GetParameters())
                {
                    string dependencyId = GetParamInjectDependency(paramInfo) ?? paramInfo.ParameterType.GetDependencyId();

                    if (this.Container.ContainsDependency(dependencyId))
                    {
                        IDependencyHolder primaryDepHolder = this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder;

                        if (primaryDepHolder == null) throw new PrimaryOrPreferredTargetDependencyNotFound("Source dependency: " + dependencyId);

                        object memberValue = primaryDepHolder.GetInstance(GetParamInjectValues(paramInfo));

                        paramValues[index] = memberValue;

                        index++;
                    }
                    else
                    {
                        string errMsg = string.Format("{0} dependency not found or is not attributed as Injectable while auto injecting the parameter '{1}' of method '{2}' of dependency: {3}", dependencyId, paramInfo.Name, methodInfo.Name, this._type.GetDependencyId());
                        throw new NonInjectableTypeException(errMsg);
                    }
                }

                var o = methodInfo.Invoke(instance, paramValues);
            }


            this.InjectNext(instance);
        }
    }
}
