using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors
{
    public abstract class AutoInjectionStrategy
    {
        protected IDIContainer _container;
        protected Type _type;

        protected AutoInjectionStrategy(IDIContainer container, Type type)
        {
            this._container = container;
            this._type = type;
        }

        public abstract AutoInjector ChainAutoInjection();
    }

    public class FMPCAutoInjectionStrategy : AutoInjectionStrategy
    {
        public FMPCAutoInjectionStrategy(IDIContainer container, Type type) : base(container, type) { }

        public override AutoInjector ChainAutoInjection()
        {
            return 
                new PublicFieldAutoInjector(
                    new MethodAutoInjector(
                        new PropertyAutoInjector(
                            new ConstructorAutoInjector(this._container, this._type)
                        , this._container, this._type)
                    , this._container, this._type),
                this._container, this._type);
        }
    }
}
