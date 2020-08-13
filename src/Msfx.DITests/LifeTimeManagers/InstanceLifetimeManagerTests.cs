using FakeTypes.For.NonDITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.AutoInjectors;
using Msfx.DI.Containers;
using Msfx.DI.LifetimeManagers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.LifetimeManagers.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class InstanceLifetimeManagerTests
    {
        [TestMethod]
        public void InstanceLifetimeManager_GetInstanceLifetimeManager_Test()
        {
            //arrage
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            InstanceLifetimeManager localInstanceLifetimeManager, staticInstanceLifetimeManager, invalidLifetimeManager;

            //act
            localInstanceLifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(mockContainer.Object, typeof(foo), InstanceType.Local);
            staticInstanceLifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(mockContainer.Object, typeof(foo), InstanceType.Static);
            invalidLifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(mockContainer.Object, typeof(foo), InstanceType.Invalid);

            //assert
            Assert.IsInstanceOfType(localInstanceLifetimeManager, typeof(LocalInstanceLifetimeManager));
            Assert.IsInstanceOfType(staticInstanceLifetimeManager, typeof(StaticInstanceLifetimeManager));
            Assert.IsNull(invalidLifetimeManager);
        }
    }
}