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
    public class PublicFieldAutoInjector : MemberAutoInjector
    {
        public PublicFieldAutoInjector(IDIContainer container, Type type) : base(container, type) { }

        public PublicFieldAutoInjector(MemberAutoInjector successor, IDIContainer container, Type type) : base(successor,container, type) { }

        public override void Inject(object instance)
        {
            //PublicField Injection

            var fieldsToAutoInject = this._type.GetMembers().Where(field => Attribute.IsDefined(field, typeof(AutoInjectAttribute)) && field.MemberType == MemberTypes.Field);

            foreach (var field in fieldsToAutoInject)
            {
                FieldInfo fieldInfo = (FieldInfo)field;
                string dependencyId = GetMemberInjectDependency(fieldInfo) ?? fieldInfo.FieldType.GetDependencyId();

                if (this.Container.ContainsDependency(dependencyId))
                {
                    IDependencyHolder primaryDepHolder = this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder;

                    if (primaryDepHolder == null) throw new PrimaryOrPreferredTargetDependencyNotFound("Source dependency: " + dependencyId);

                    object memberValue = primaryDepHolder.GetInstance(GetMemberInjectValues(fieldInfo));

                    fieldInfo.SetValue(instance, memberValue);
                }
                else
                {
                    string errMsg = string.Format("{0} dependency not found or is not attributed as Injectable while auto injecting the field '{1}' of dependency: {2}", dependencyId, fieldInfo.Name ,this._type.GetDependencyId());
                    throw new NonInjectableTypeException(errMsg);
                }
            }

             this.InjectNext(instance);
        }
    }
}
