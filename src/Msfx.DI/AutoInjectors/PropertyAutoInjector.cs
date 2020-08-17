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
    public class PropertyAutoInjector : MemberAutoInjector
    {
        public PropertyAutoInjector(IDIContainer container, Type type) :base(container,type){ }

        public PropertyAutoInjector(MemberAutoInjector successor, IDIContainer container, Type type) : base(successor,container, type) { }

        public override void Inject(object instance)
        {
            //Property Injection

            var propsToAutoInject = this._type.GetMembers().Where(prop => Attribute.IsDefined(prop, typeof(AutoInjectAttribute)) && prop.MemberType == MemberTypes.Property);

            foreach (var prop in propsToAutoInject)
            {
                PropertyInfo propInfo = (PropertyInfo)prop;
                string dependencyId = GetMemberInjectDependency(propInfo) ?? propInfo.PropertyType.GetDependencyId();

                if (this.Container.ContainsDependency(dependencyId))
                {
                    IDependencyHolder primaryDepHolder = this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder;

                    if (primaryDepHolder == null) throw new PrimaryOrPreferredTargetDependencyNotFound("Source dependency: " + dependencyId);

                    object memberValue = primaryDepHolder.GetInstance(GetMemberInjectValues(propInfo));

                    propInfo.SetValue(instance, memberValue);
                }
                else
                {
                    string errMsg = string.Format("{0} dependency not found or is not attributed as Injectable while auto injecting the property '{1}' of dependency: {2}", dependencyId, propInfo.Name,this._type.GetDependencyId());
                    throw new NonInjectableTypeException(errMsg);
                }
            }

            this.InjectNext(instance);
        }
    }
}
