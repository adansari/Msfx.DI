using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Msfx.DI.Samples.MVC
{
    public class DIControllerFactory : DefaultControllerFactory
    {
        private DIContext _ctxDI;

        public DIControllerFactory(DIContext ctxDI)
        {
            this._ctxDI = ctxDI;
            this.AutoInjectionStrategy = Msfx.DI.AutoInjectors.AutoInjectionStrategies.FMPCAutoInjection;
        }

        public Msfx.DI.AutoInjectors.AutoInjectionStrategies AutoInjectionStrategy { get; set; }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            IController controller = base.CreateController(requestContext, controllerName);

            Type controllerType = GetControllerType(requestContext, controllerName);

            Msfx.DI.AutoInjectors.AutoInjectionStrategy
               .GetStrategy(AutoInjectionStrategy, this._ctxDI.Container, controllerType)
                .ChainAutoInjectors()
                 .Inject(controller);

            return controller;
        }
    }
}