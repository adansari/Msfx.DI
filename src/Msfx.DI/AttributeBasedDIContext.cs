using Msfx.DI.Registrars;
using Msfx.DI.Scanners;
using System;

namespace Msfx.DI
{
    public class AttributeBasedDIContext : DIContext
    {
        public AttributeBasedDIContext(Type callingType) :base(callingType)
        {
            this.Scanner = DependencyScanner.GetDependencyScanner(this._scanTarget,this._callingAssembly,this._currentNamespace);
            this.Registrar = DependencyRegistrar.BuildRegistrarChain(this._container);
        }

        public AttributeBasedDIContext(Type callingType, DependencyScanTarget scanTarget) : base(callingType, scanTarget)
        {
            this.Scanner = DependencyScanner.GetDependencyScanner(this._scanTarget, this._callingAssembly, this._currentNamespace);
            this.Registrar = DependencyRegistrar.BuildRegistrarChain(this._container);
        }
        public virtual DependencyScanner Scanner { get; }

        public virtual DependencyRegistrar Registrar { get; }

        public override DIContext Scan()
        {
            var typesScanned = this.Scanner.Scan();
            
            this.Registrar.Register(typesScanned);

            return this;

        }
    }
}
