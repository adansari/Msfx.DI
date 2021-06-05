using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Registrars
{
    public abstract class DependencyRegistrar
    {
        protected DependencyRegistrar _successor;
        protected bool _canMoveNext = true;
        protected IDIContainer _container;

        protected DependencyRegistrar(IDIContainer container)
        {
            this._successor = null;
            this._container = container;
        }

        protected DependencyRegistrar(DependencyRegistrar successor, IDIContainer container):this(container)
        {
            this._successor = successor;
        }

        public virtual IDIContainer Container { get { return this._container; } }
        public virtual DependencyRegistrar Successor { get { return this._successor; } }
        public virtual bool CanMoveNext{ get { return this._canMoveNext; } }

        public virtual void RegisterNext(IEnumerable<Type> types)
        {
            if(this.Successor!=null && this.CanMoveNext)
            {
                this.Successor.Register(types);
            }
        }

        public abstract void Register(IEnumerable<Type> types);

        public static DependencyRegistrar BuildRegistrarChain(IDIContainer dIContainer)
        {
            return new InjectableDependencyRegistrar(
                    new InjectForDependencyRegistrar(
                        new CanBeInjectedDependencyRegistrar(
                            null, dIContainer)
                        , dIContainer)
                    , dIContainer);
        }
    }
}
