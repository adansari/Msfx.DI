﻿using FakeTypes.For.DITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Containers;
using Msfx.DI.LifetimeManagers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DependencyHolderTests
    {
        [TestMethod]
        public void DependencyHolder_GetInstance_Test()
        {
            //arrange
            Type fakeType = typeof(foo);

            Mock<InstanceLifetimeManager> mockInstanceLifetimeManager = new Mock<InstanceLifetimeManager>(It.IsAny<IDIContainer>(),It.IsAny<Type>());
            mockInstanceLifetimeManager.Setup(ltm => ltm.CreateOrGetInstance(It.IsAny<Object[]>())).Verifiable();

            Mock<DependencyHolder> mockDHolder = new Mock<DependencyHolder>(It.IsAny<IDIContainer>(),fakeType) { CallBase = true };
            mockDHolder.SetupGet(dh => dh.LifetimeManager).Returns(mockInstanceLifetimeManager.Object);

            //act
            mockDHolder.Object.GetInstance(It.IsAny<object[]>());

            //assert
            mockInstanceLifetimeManager.Verify(ltm => ltm.CreateOrGetInstance(It.IsAny<Object[]>()));
        }
    }
}
