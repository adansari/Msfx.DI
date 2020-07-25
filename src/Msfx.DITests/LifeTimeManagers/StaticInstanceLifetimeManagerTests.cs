using FakeTypes.For.DITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Factories;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.LifeTimeManagers.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StaticInstanceLifetimeManagerTests
    {
        [TestMethod]
        public void StaticInstanceLifetimeManager_CreateOrGetInstance_Dont_Create_Test()
        {
            //arrage
            foo afoo = new foo() { strProp = "state"};
            
            Mock<InstanceFactory> mockFactory = new Mock<InstanceFactory>();
            Mock<StaticInstanceLifetimeManager> mockLifetimeManager = new Mock<StaticInstanceLifetimeManager>(typeof(foo)) { CallBase = true };
            mockLifetimeManager.SetupGet(ltm => ltm.Factory).Returns(mockFactory.Object);
            mockLifetimeManager.SetupGet(ltm => ltm.Instance).Returns(afoo);

            //act
            foo anotherfoo = (foo)mockLifetimeManager.Object.CreateOrGetInstance(It.IsAny<Object[]>());

            //assert
            mockFactory.Verify(factory => factory.CreateInstance(typeof(foo), It.IsAny<Object[]>()), Times.Never());
            Assert.AreSame(afoo,anotherfoo);
            Assert.AreEqual(afoo.strProp, anotherfoo.strProp);
        }

        [TestMethod]
        public void StaticInstanceLifetimeManager_CreateOrGetInstance_Create_Test()
        {
            //arrage
            Mock<InstanceFactory> mockFactory = new Mock<InstanceFactory>();
            Mock<StaticInstanceLifetimeManager> mockLifetimeManager = new Mock<StaticInstanceLifetimeManager>(typeof(foo)) { CallBase = true };
            mockLifetimeManager.SetupGet(ltm => ltm.Factory).Returns(mockFactory.Object);
            mockLifetimeManager.SetupGet(ltm => ltm.Instance).Returns((foo)null);

            //act
            mockLifetimeManager.Object.CreateOrGetInstance(It.IsAny<Object[]>());

            //assert
            mockFactory.Verify(factory => factory.CreateInstance(typeof(foo), It.IsAny<Object[]>()), Times.Exactly(1));
        }
    }
}