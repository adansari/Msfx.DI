using Msfx.DI.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.LifeTimeManagers
{
    public class LocalInstanceLifetimeManager: InstanceLifetimeManager
    {
        public LocalInstanceLifetimeManager(Type type):base(type){ }
        
        public override object CreateOrGetInstance(object[] args)
        {
            return this.Factory.CreateInstance(this.Type, args);
        }
    }
}
