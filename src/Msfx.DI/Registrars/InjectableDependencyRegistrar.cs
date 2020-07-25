using Msfx.DI.Attributes;
using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Registrars
{
    public class InjectableDependencyRegistrar : DependencyRegistrar
    {
        public InjectableDependencyRegistrar(IDIContainer container) : base(container)
        { }
        public InjectableDependencyRegistrar(DependencyRegistrar successor, IDIContainer container) : base(successor,container)
        { }
        public override void Register(IEnumerable<Type> types)
        {
            foreach (Type t in types)
            {
                if (Attribute.IsDefined(t, typeof(InjectableAttribute)))
                {
                    InjectableAttribute typeInjectableAttribute = Attribute.GetCustomAttributes(t, typeof(InjectableAttribute))
                                                                           .FirstOrDefault() as InjectableAttribute;

                    this.Container.Register(t.GetDependencyId(), t, typeInjectableAttribute.InstanceType);
                }
            }

            this.RegisterNext(types);
        }
    }
}
