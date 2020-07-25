using FakeTypes.For.NonDITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Factories;
using Msfx.DI.LifeTimeManagers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Msfx.DI.LifeTimeManagers.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class LocalInstanceLifetimeManagerTests
    {
        [TestMethod]
        public void LocalInstanceLifetimeManager_CreateOrGetInstance_Test()
        {
            //arrage
            Mock<InstanceFactory> mockFactory = new Mock<InstanceFactory>();
            Mock<LocalInstanceLifetimeManager> mockLifetimeManager = new Mock<LocalInstanceLifetimeManager>(typeof(foo)) { CallBase = true };
            mockLifetimeManager.SetupGet(ltm => ltm.Factory).Returns(mockFactory.Object);

            //act
            mockLifetimeManager.Object.CreateOrGetInstance(It.IsAny<Object[]>());

            //assert
            mockFactory.Verify(factory=> factory.CreateInstance(typeof(foo),It.IsAny<Object[]>()),Times.Exactly(1));
        }
    }
}