using Msfx.DI.Attributes;
using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors
{
    public class PropertyAutoInjector : AutoInjector
    {
        public PropertyAutoInjector(IDIContainer container, Type type) :base(container,type){ }

        public PropertyAutoInjector(AutoInjector successor, IDIContainer container, Type type) : base(container, type) { }

        public override void Inject(object instance)
        {
            //Property Injection

            var propsToAutoInject = this._type.GetMembers().Where(prop => Attribute.IsDefined(prop, typeof(AutoInjectAttribute)) && prop.MemberType == MemberTypes.Property);

            foreach (var prop in propsToAutoInject)
            {
                PropertyInfo propInfo = (PropertyInfo)prop;
                string dependencyId = propInfo.PropertyType.GetDependencyId();

                if (this.Container.ContainsDependency(dependencyId))
                {
                    object memberValue = this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder.GetInstance(null);

                    propInfo.SetValue(instance, memberValue);
                }
            }

            this.InjectNext(instance);
        }
    }
}
