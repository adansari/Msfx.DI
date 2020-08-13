﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class AutoInjectionStrategyTests
    {
        [TestMethod]
        public void AutoInjectionStrategy_GetStrategy_Test()
        {
            //arrange
            AutoInjectionStrategy autoInjectionStrategy, invalidAutoInjectionStrategy;

            //act
            autoInjectionStrategy = AutoInjectionStrategy.GetStrategy(AutoInjectionStrategies.FMPCAutoInjection,It.IsAny<IDIContainer>(),It.IsAny<Type>());
            invalidAutoInjectionStrategy = AutoInjectionStrategy.GetStrategy(AutoInjectionStrategies.Invalid, It.IsAny<IDIContainer>(), It.IsAny<Type>());

            //assert
            Assert.IsInstanceOfType(autoInjectionStrategy, typeof(FMPCAutoInjectionStrategy));
            Assert.IsNull(invalidAutoInjectionStrategy);
        }
    }
}