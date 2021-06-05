using FakeAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Containers;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Registrars.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DependencyRegistrarTests
    {
        Mock<IDIContainer> mockDIContainer;
        Mock<DependencyRegistrar> mockSuccessorDepRegistrar;
        Mock<DependencyRegistrar> mockDepRegistrar;

       [TestInitialize]
        public void Init()
        {
            mockDIContainer = new Mock<IDIContainer>();
            mockSuccessorDepRegistrar = new Mock<DependencyRegistrar>(mockDIContainer.Object);
            mockDepRegistrar = new Mock<DependencyRegistrar>(mockSuccessorDepRegistrar.Object, mockDIContainer.Object) { CallBase = true };
        }

        [TestMethod]
        public void DependencyRegistrar_Get_CanMoveNext_Test()
        {
            //arrange


            //act


            //assert
            Assert.IsTrue(mockDepRegistrar.Object.CanMoveNext);
        }

        [TestMethod]
        public void DependencyRegistrar_RegisterNext_Test()
        {
            //arrange
            mockDepRegistrar.SetupGet(dr => dr.CanMoveNext).Returns(true);

            //act
            mockDepRegistrar.Object.RegisterNext(MockAssembly.NonInjectableTypes);

            //assert
            mockSuccessorDepRegistrar.Verify(dr=>dr.Register(MockAssembly.NonInjectableTypes));
        }

        [TestMethod]
        public void DependencyRegistrar_Not_RegisterNext_Test()
        {
            //arrange
            mockDepRegistrar.SetupGet(dr => dr.CanMoveNext).Returns(false);

            //act
            mockDepRegistrar.Object.RegisterNext(MockAssembly.NonInjectableTypes);

            //assert
            mockSuccessorDepRegistrar.Verify(dr => dr.Register(MockAssembly.NonInjectableTypes), Times.Never);
        }

        [TestMethod]
        public void DependencyRegistrar_RegisterNext_Null_Successor_Test()
        {
            //arrange
            mockDepRegistrar = new Mock<DependencyRegistrar>(mockDIContainer.Object) { CallBase = true };
            mockDepRegistrar.SetupGet(dr => dr.CanMoveNext).Returns(true);

            //act
            mockDepRegistrar.Object.RegisterNext(MockAssembly.NonInjectableTypes);

            //assert
            Assert.IsNull(mockDepRegistrar.Object.Successor);
        }

        [TestMethod]
        public void DependencyRegistrar_BuildRegistrarChain_Test()
        {
            //arrange 
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();

            //act
            DependencyRegistrar dependencyRegistrar = DependencyRegistrar.BuildRegistrarChain(mockContainer.Object);

            //assert
            Assert.IsNotNull(dependencyRegistrar);
            Assert.IsInstanceOfType(dependencyRegistrar, typeof(InjectableDependencyRegistrar));

            Assert.IsNotNull(dependencyRegistrar.Successor);
            Assert.IsInstanceOfType(dependencyRegistrar.Successor, typeof(InjectForDependencyRegistrar));

            Assert.IsNotNull(dependencyRegistrar.Successor.Successor);
            Assert.IsInstanceOfType(dependencyRegistrar.Successor.Successor, typeof(CanBeInjectedDependencyRegistrar));
        }
    }
}