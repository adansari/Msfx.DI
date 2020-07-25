using FakeAssembly;
using FakeTypes.For.DITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Containers;
using Msfx.DI.Registrars;
using Msfx.DI.Scanners;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AttributeBasedDIContextTests
    {
        Mock<ConcurrentDictDIContainer> mockDIContainer;
        Mock<DependencyScanner> mockScanner;
        Mock<DependencyRegistrar> mockRegistrar;
        Mock<AttributeBasedDIContext> mockAttrDICtx;
        
        [TestInitialize]
        public void Init()
        {
            mockDIContainer = new Mock<ConcurrentDictDIContainer>();

            mockScanner = new Mock<DependencyScanner>(MockAssembly.GetMock().Object);
            mockScanner.Setup(scanner => scanner.Scan()).Verifiable();

            mockRegistrar = new Mock<DependencyRegistrar>(mockDIContainer.Object);
            mockRegistrar.Setup(registrar => registrar.Register(It.IsAny<Type[]>())).Verifiable();
        }

        [TestMethod]
        public void AttributeBasedDIContext_Ctor1_Test()
        {
            //arrange
            AttributeBasedDIContext diCtx;

            //act
            diCtx = new AttributeBasedDIContext(typeof(Boot));

            //assert
            Assert.IsNotNull(diCtx.Registrar);
            Assert.IsNotNull(diCtx.Scanner);
        }

        [TestMethod]
        public void AttributeBasedDIContext_Ctor2_Test()
        {
            //arrange
            AttributeBasedDIContext diCtx;

            //act
            diCtx = new AttributeBasedDIContext(typeof(Boot), DependencyScanTarget.CurrentNamespaceRecursive);

            //assert
            Assert.IsNotNull(diCtx.Registrar);
            Assert.IsNotNull(diCtx.Scanner);
        }

        [TestMethod]
        public void AttributeBasedDIContext_Scan_Test()
        {
            //arrange
            mockAttrDICtx = new Mock<AttributeBasedDIContext>(typeof(Boot)) { CallBase = true };
            mockAttrDICtx.SetupGet(diCtx => diCtx.Scanner).Returns(mockScanner.Object);
            mockAttrDICtx.SetupGet(diCtx => diCtx.Registrar).Returns(mockRegistrar.Object);

            //act
            mockAttrDICtx.Object.Scan();

            //assert
            mockScanner.Verify(scanner => scanner.Scan(), Times.Exactly(1));
            mockRegistrar.Verify(registrar => registrar.Register(It.IsAny<Type[]>()),Times.Exactly(1));
        }

        [TestMethod]
        public void AttributeBasedDIContext_Scan_Assembly_Test()
        {
            //arrange
            mockAttrDICtx = new Mock<AttributeBasedDIContext>(typeof(Boot), DependencyScanTarget.Assembly) { CallBase = true };
            mockAttrDICtx.SetupGet(diCtx => diCtx.Scanner).Returns(mockScanner.Object);
            mockAttrDICtx.SetupGet(diCtx => diCtx.Registrar).Returns(mockRegistrar.Object);

            //act
            mockAttrDICtx.Object.Scan();

            //assert
            mockScanner.Verify(scanner => scanner.Scan(), Times.Exactly(1));
            mockRegistrar.Verify(registrar => registrar.Register(It.IsAny<Type[]>()), Times.Exactly(1));
        }

        [TestMethod]
        public void AttributeBasedDIContext_Get_Scanner_And_Registrar_Test()
        {
            //arrange
            AttributeBasedDIContext attrDICtx;

            //act
            attrDICtx = new AttributeBasedDIContext(typeof(Boot));

            //assert
            Assert.IsNotNull(attrDICtx.Scanner);
            Assert.IsNotNull(attrDICtx.Registrar);
        }
    }
}