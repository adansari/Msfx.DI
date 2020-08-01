using FakeTypes.For.InjectableDependencyRegistrarTests;
using FakeTypes.For.NonDITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Containers;
using Msfx.DI.LifetimeManagers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Registrars.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class InjectableDependencyRegistrarTests
    {
        Mock<IDIContainer> mockDIContainer;
        Mock<InjectableDependencyRegistrar> mockInjectableDepRegistrar;

        [TestInitialize]
        public void Init()
        {
            mockDIContainer = new Mock<IDIContainer>();
            mockInjectableDepRegistrar = new Mock<InjectableDependencyRegistrar>(mockDIContainer.Object) { CallBase = true };
        }

        [TestMethod]
        public void InjectableDependencyRegistrar_Register_Test()
        {
            //arrange
            Type[] types = new Type[] { typeof(Animal), typeof(IAnimal), typeof(Dog) };

            //act
            mockInjectableDepRegistrar.Object.Register(types);

            //assert
            mockDIContainer.Verify(c=>c.Register(It.IsAny<string>(),It.IsAny<Type>(),It.IsAny<InstanceType>()));
            mockInjectableDepRegistrar.Verify(idr=>idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
        }

        [TestMethod]
        public void InjectableDependencyRegistrar_Dont_Register_Test()
        {
            //arrange
            Type[] types = new Type[] { typeof(foo), typeof(abs_foo), typeof(Ifoo) };

            //act
            mockInjectableDepRegistrar.Object.Register(types);

            //assert
            mockDIContainer.Verify(c => c.Register(It.IsAny<string>(), It.IsAny<Type>(), It.IsAny<InstanceType>()),Times.Never());
            mockInjectableDepRegistrar.Verify(idr => idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
        }
    }
}