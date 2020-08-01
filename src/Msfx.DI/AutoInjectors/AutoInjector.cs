using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors
{
    public abstract class AutoInjector
    {
        protected AutoInjector _successor;
        protected bool _canMoveNext = true;
        protected IDIContainer _container;
        protected Type _type;

        protected AutoInjector(IDIContainer container, Type type)
        {
            this._successor = null;
            this._container = container;
            this._type = type;
        }

        protected AutoInjector(AutoInjector successor, IDIContainer container, Type type) : this(container, type)
        {
            this._successor = successor;
        }

        public virtual IDIContainer Container { get { return this._container; } }
        public virtual AutoInjector Successor { get { return this._successor; } }

        public virtual bool CanMoveNext { get { return this._canMoveNext; } }

        public virtual void InjectNext(object instance)
        {
            if (this.Successor != null && this.CanMoveNext)
            {
                this.Successor.Inject(instance);
            }
        }

        public abstract void Inject(object instance);
    }
}
