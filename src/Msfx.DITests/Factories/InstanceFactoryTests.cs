using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msfx.DI.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Factories.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class InstanceFactoryTests
    {
        [TestMethod]
        public void InstanceFactory_GetFactory_Test()
        {
            Assert.IsInstanceOfType(InstanceFactory.GetFactory(), typeof(ActivatorInstanceFactory));
        }
    }
}