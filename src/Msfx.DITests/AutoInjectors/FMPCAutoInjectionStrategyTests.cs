using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.AutoInjectors;
using Msfx.DI.Containers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class FMPCAutoInjectionStrategyTests
    {
        [TestMethod]
        public void FMPCAutoInjectionStrategy_ChainAutoInjectionTest()
        {
            //arrange
            FMPCAutoInjectionStrategy fMPCAutoInjection = new FMPCAutoInjectionStrategy(It.IsAny<IDIContainer>(),It.IsAny<Type>());

            //act
            AutoInjector autoInjector = fMPCAutoInjection.ChainAutoInjection();


            //assert
            Assert.IsInstanceOfType(autoInjector,typeof(PublicFieldAutoInjector));
            Assert.IsInstanceOfType(autoInjector.Successor, typeof(MethodAutoInjector));
            Assert.IsInstanceOfType(autoInjector.Successor.Successor, typeof(PropertyAutoInjector));
            Assert.IsInstanceOfType(autoInjector.Successor.Successor.Successor, typeof(ConstructorAutoInjector));
        }
    }
}