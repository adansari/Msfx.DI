using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors
{
    public class MethodAutoInjector : AutoInjector
    {
        public MethodAutoInjector(IDIContainer container, Type type) :base(container,type){ }

        public MethodAutoInjector(AutoInjector successor, IDIContainer container, Type type) : base(container, type) { }

        public override void Inject(object instance)
        {
            //Method Injection

            this.InjectNext(instance);
        }
    }
}
