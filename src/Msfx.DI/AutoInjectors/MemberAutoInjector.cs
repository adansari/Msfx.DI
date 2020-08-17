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
    public abstract class MemberAutoInjector
    {
        protected MemberAutoInjector _successor;
        protected bool _canMoveNext = true;
        protected IDIContainer _container;
        protected Type _type;

        protected MemberAutoInjector(IDIContainer container, Type type)
        {
            this._successor = null;
            this._container = container;
            this._type = type;
        }

        protected MemberAutoInjector(MemberAutoInjector successor, IDIContainer container, Type type) : this(container, type)
        {
            this._successor = successor;
        }

        public virtual IDIContainer Container { get { return this._container; } }
        public virtual MemberAutoInjector Successor { get { return this._successor; } }

        public virtual bool CanMoveNext { get { return this._canMoveNext; } }

        public virtual void InjectNext(object instance)
        {
            if (this.Successor != null && this.CanMoveNext)
            {
                this.Successor.Inject(instance);
            }
        }

        public abstract void Inject(object instance);

        public virtual string GetMemberInjectDependency(MemberInfo memberInfo)
        {
            if (Attribute.IsDefined(memberInfo, typeof(InjectAttribute)))
            {
                InjectAttribute injectTypeAttribute = Attribute.GetCustomAttributes(memberInfo, typeof(InjectAttribute))
                                                                       .FirstOrDefault() as InjectAttribute;

                return injectTypeAttribute.Type.GetDependencyId();
            }

            return null;
        }

        public virtual string GetParamInjectDependency(ParameterInfo paramInfo)
        {
            if (Attribute.IsDefined(paramInfo, typeof(InjectAttribute)))
            {
                InjectAttribute injectTypeAttribute = Attribute.GetCustomAttributes(paramInfo, typeof(InjectAttribute))
                                                                       .FirstOrDefault() as InjectAttribute;

                return injectTypeAttribute.Type.GetDependencyId();
            }

            return null;
        }

       
        public virtual object[] GetMemberInjectValues(MemberInfo memberInfo)
        {
            if (Attribute.IsDefined(memberInfo, typeof(InjectValueAttribute)))
            {
                InjectValueAttribute memberInjectValueAttribute = Attribute.GetCustomAttributes(memberInfo, typeof(InjectValueAttribute))
                                                                       .FirstOrDefault() as InjectValueAttribute;

                return memberInjectValueAttribute.Values;
            }

            return null;
        }

        public virtual object[] GetParamInjectValues(ParameterInfo paramInfo)
        {
            if (Attribute.IsDefined(paramInfo, typeof(InjectValueAttribute)))
            {
                InjectValueAttribute paramInjectValueAttribute = Attribute.GetCustomAttributes(paramInfo, typeof(InjectValueAttribute))
                                                                       .FirstOrDefault() as InjectValueAttribute;

                return paramInjectValueAttribute.Values;
            }

            return null;
        }
    }
}
