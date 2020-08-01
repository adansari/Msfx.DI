using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors
{
    public class ConstructorAutoInjector : AutoInjector
    {
        public ConstructorAutoInjector(IDIContainer container, Type type) :base(container,type){ }

        public ConstructorAutoInjector(AutoInjector successor, IDIContainer container, Type type) : base(container, type) { }

        public override void Inject(object instance)
        {
            //Constructor Injection

            this.InjectNext(instance);
        }
    }
}
