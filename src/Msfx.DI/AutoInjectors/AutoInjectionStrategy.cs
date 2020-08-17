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

        public abstract MemberAutoInjector ChainAutoInjectors();

        public static AutoInjectionStrategy GetStrategy(AutoInjectionStrategies autoInjectionStrategy, IDIContainer container, Type type)
        {
            switch (autoInjectionStrategy)
            {
                case AutoInjectionStrategies.FMPCAutoInjection:
                    return new FMPCAutoInjectionStrategy(container, type);
                case AutoInjectionStrategies.CFPMAutoInjection:
                    return new CFPMAutoInjectionStrategy(container, type);
                default:
                    return null;
            }
        }
    }

    public class FMPCAutoInjectionStrategy : AutoInjectionStrategy
    {
        public FMPCAutoInjectionStrategy(IDIContainer container, Type type) : base(container, type) { }

        public override MemberAutoInjector ChainAutoInjectors()
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

    public class CFPMAutoInjectionStrategy : AutoInjectionStrategy
    {
        public CFPMAutoInjectionStrategy(IDIContainer container, Type type) : base(container, type) { }

        public override MemberAutoInjector ChainAutoInjectors()
        {
            return
                new ConstructorAutoInjector(
                    new PublicFieldAutoInjector(
                        new PropertyAutoInjector(
                            new MethodAutoInjector(this._container, this._type)
                        , this._container, this._type)
                    , this._container, this._type),
                this._container, this._type);
        }
    }

    public enum AutoInjectionStrategies
    {
        //Sequence of autoinjection => Field => Method => Property => Ctor  
        FMPCAutoInjection,
        //Sequence of autoinjection => Ctor => Field => Property => Method 
        CFPMAutoInjection,
        Invalid
    }
}
