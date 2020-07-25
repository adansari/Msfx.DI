using Msfx.DI.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.LifeTimeManagers
{
    public abstract class InstanceLifetimeManager
    {
        protected Type _type;
        protected InstanceFactory _factory;
        protected InstanceLifetimeManager(Type type)
        {
            this._type = type;
            this._factory = InstanceFactory.GetFactory();
        }

        public virtual Type Type { get { return this._type; } }

        public virtual InstanceFactory Factory { get { return this._factory; } }

        public abstract object CreateOrGetInstance(object[] args);

        public static InstanceLifetimeManager GetInstanceLifetimeManager(Type type,InstanceType instanceType)
        {
            switch (instanceType)
            {
                case InstanceType.Static:
                    return new StaticInstanceLifetimeManager(type);
                case InstanceType.Local:
                    return new LocalInstanceLifetimeManager(type);
                default:
                    return null;
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
