using Msfx.DI.Attributes;
using Msfx.DI.Containers;
using Msfx.DI.Factories;
using System;
using System.Linq;
using System.Reflection;

namespace Msfx.DI.LifetimeManagers
{
    public abstract class InstanceLifetimeManager
    {
        protected Type _type;
        protected InstanceFactory _factory;

        protected InstanceLifetimeManager(IDIContainer container,Type type)
        {
            this._type = type;
            this._factory = InstanceFactory.GetFactory();
            this.Container = container;
        }

        public virtual Type Type { get { return this._type; } }

        public IDIContainer Container { get; }

        public virtual InstanceFactory Factory { get { return this._factory; } }

        public abstract object CreateOrGetInstance(object[] args);

        public static InstanceLifetimeManager GetInstanceLifetimeManager(IDIContainer container,Type type,InstanceType instanceType)
        {
            switch (instanceType)
            {
                case InstanceType.Static:
                    return new StaticInstanceLifetimeManager(container,type);
                case InstanceType.Local:
                    return new LocalInstanceLifetimeManager(container,type);
                default:
                    return null;
            }
        }

        public void AutoInjectMembers(object instance)
        {
            var membersToAutoInject = Type.GetMembers().Where(prop => Attribute.IsDefined(prop, typeof(AutoInjectAttribute)));

            foreach (var member in membersToAutoInject)
            {
                if (member.MemberType == MemberTypes.Field)
                {
                    FieldInfo field = (FieldInfo)member;
                    string dependencyId = field.FieldType.GetDependencyId();

                    if (this.Container.ContainsDependency(dependencyId))
                    {
                        object memberValue = this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder.GetInstance(null);

                        field.SetValue(instance, memberValue);
                    }
                }

                if (member.MemberType == MemberTypes.Constructor)
                {
                    ConstructorInfo ctor = (ConstructorInfo)member;
                    object[] paramValues = new object[ctor.GetParameters().Length];
                    int index = 0;

                    foreach (ParameterInfo parameter in ctor.GetParameters())
                    {
                        string dependencyId = parameter.ParameterType.GetDependencyId();

                        if (this.Container.ContainsDependency(dependencyId))
                        {
                            paramValues[index] = this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder.GetInstance(null);

                            index++;
                        }
                    }

                    var o = ctor.Invoke(instance, paramValues);
                }

                if (member.MemberType == MemberTypes.Method)
                {
                    MethodInfo method = (MethodInfo)member;
                    object[] paramValues = new object[method.GetParameters().Length];
                    int index = 0;

                    foreach (ParameterInfo parameter in method.GetParameters())
                    {
                        string dependencyId = parameter.ParameterType.GetDependencyId();

                        if (this.Container.ContainsDependency(dependencyId))
                        {
                            paramValues[index] = this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder.GetInstance(null);

                            index++;
                        }
                    }

                    var o = method.Invoke(instance, paramValues);
                }
            }
        }
    }

    public enum InstanceType
    {
        Static,
        Local,
        Invalid
    }
}
