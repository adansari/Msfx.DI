using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors
{
    public class PublicFieldAutoInjector : AutoInjector
    {
        public PublicFieldAutoInjector(IDIContainer container, Type type) : base(container, type) { }

        public PublicFieldAutoInjector(AutoInjector successor, IDIContainer container, Type type) : base(container, type) { }

        public override void Inject(object instance)
        {
            //PublicField Injection

            this.InjectNext(instance);
        }
    }
}
