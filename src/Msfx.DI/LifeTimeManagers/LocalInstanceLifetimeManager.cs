using Msfx.DI.Containers;
using System;

namespace Msfx.DI.LifetimeManagers
{
    public class LocalInstanceLifetimeManager: InstanceLifetimeManager
    {
        public LocalInstanceLifetimeManager(IDIContainer container,Type type):base(container,type) { }
        
        public override object CreateOrGetInstance(object[] args)
        {
            object instance = this.Factory.CreateInstance(this.Type, args);

            return instance;
        }
    }
}
