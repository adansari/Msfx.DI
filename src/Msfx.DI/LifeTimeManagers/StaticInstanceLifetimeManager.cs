using Msfx.DI.Containers;
using Msfx.DI.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.LifetimeManagers
{
    public class StaticInstanceLifetimeManager : InstanceLifetimeManager
    {
        private static readonly object _instancelock = new object();

        public StaticInstanceLifetimeManager(IDIContainer container, Type type):base(container,type){}

        public virtual object Instance { get; private set; }

        public override object CreateOrGetInstance(object[] args)
        {
            if (this.Instance == null)
            {
                lock (_instancelock)
                {
                    if (this.Instance == null)
                    {
                        this.Instance = this.Factory.CreateInstance(this.Type, args);

                        InstanceCreatedEventArgs arg = new InstanceCreatedEventArgs();
                        arg.Instance = this.Instance;
                        arg.InstanceType = this.Type;

                        OnInstanceCreated(arg);
                    }
                }
            }

            return this.Instance;
        }
    }
}
