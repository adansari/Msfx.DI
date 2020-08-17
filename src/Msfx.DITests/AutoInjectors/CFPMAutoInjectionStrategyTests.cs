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
    public class CFPMAutoInjectionStrategyTests
    {
        [TestMethod]
        public void CFPMAutoInjectionStrategyTests_ChainAutoInjectionTest()
        {
            //arrange
            CFPMAutoInjectionStrategy cFPMAutoInjection= new CFPMAutoInjectionStrategy(It.IsAny<IDIContainer>(),It.IsAny<Type>());

            //act
            MemberAutoInjector autoInjector = cFPMAutoInjection.ChainAutoInjectors();


            //assert
            Assert.IsInstanceOfType(autoInjector,typeof(ConstructorAutoInjector));
            Assert.IsInstanceOfType(autoInjector.Successor, typeof(PublicFieldAutoInjector));
            Assert.IsInstanceOfType(autoInjector.Successor.Successor, typeof(PropertyAutoInjector));
            Assert.IsInstanceOfType(autoInjector.Successor.Successor.Successor, typeof(MethodAutoInjector));
        }
    }
}