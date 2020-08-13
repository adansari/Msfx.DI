using Msfx.DI.Attributes;
using Msfx.DI.AutoInjectors;
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
        protected AutoInjectionStrategy _autoInjectionStrategy;

        protected InstanceLifetimeManager(IDIContainer container,Type type)
        {
            this._type = type;
            this._factory = InstanceFactory.GetFactory();
            this._autoInjectionStrategy = AutoInjectionStrategy.GetStrategy(AutoInjectionStrategies.FMPCAutoInjection,container,type);
            this.Container = container;
        }

        public virtual Type Type { get { return this._type; } }

        public IDIContainer Container { get; }

        public virtual InstanceFactory Factory { get { return this._factory; } }

        public virtual AutoInjectionStrategy InjectionStrategy { get { return this._autoInjectionStrategy; } }

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

        protected virtual void OnInstanceCreated(InstanceCreatedEventArgs e)
        {
            this.InjectionStrategy.ChainAutoInjection().Inject(e.Instance);
        }
    }

    public enum InstanceType
    {
        Static,
        Local,
        Invalid
    }
}
