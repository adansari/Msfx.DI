using Msfx.DI.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.LifeTimeManagers
{
    public class StaticInstanceLifetimeManager : InstanceLifetimeManager
    {
        private static readonly object _instancelock = new object();

        public StaticInstanceLifetimeManager(Type type):base(type){}

        public virtual object Instance { get; private set; }

        public override object CreateOrGetInstance(object[] args)
        {
            if (this.Instance == null)
            {
                lock (_instancelock)
                {
                    if (this.Instance == null)
                        this.Instance = this.Factory.CreateInstance(this.Type, args);
                }
            }

            return this.Instance;
        }
    }
}
