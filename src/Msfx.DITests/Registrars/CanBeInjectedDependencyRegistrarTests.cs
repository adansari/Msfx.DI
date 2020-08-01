using FakeTypes.For.CanBeInjectedDependencyRegistrarTests;
using FakeTypes.For.NonDITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Containers;
using Msfx.DI.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Registrars.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CanBeInjectedDependencyRegistrarTests
    {
        ConcurrentDictionary<string, IDependencyMap> fakeConDict;
        Mock<ConcurrentDictDIContainer> mockDIContainer;
        Mock<CanBeInjectedDependencyRegistrar> mockCanBeInjectableRegistrar;

        [TestInitialize]
        public void Init()
        {
            fakeConDict = new ConcurrentDictionary<string, IDependencyMap>();
            fakeConDict.TryAdd(typeof(Animal).GetDependencyId(), new DependencyMap(It.IsAny<IDIContainer>(), typeof(Animal)));
            fakeConDict.TryAdd(typeof(Monkey).GetDependencyId(), new DependencyMap(It.IsAny<IDIContainer>(), typeof(Monkey)));
            fakeConDict.TryAdd(typeof(Donkey).GetDependencyId(), new DependencyMap(It.IsAny<IDIContainer>(), typeof(Donkey)));
            fakeConDict.TryAdd(typeof(Dog).GetDependencyId(), new DependencyMap(It.IsAny<IDIContainer>(), typeof(Dog)));

            mockDIContainer = new Mock<ConcurrentDictDIContainer>();
            mockDIContainer.SetupGet(c => c.Container).Returns(fakeConDict);
            mockDIContainer.Setup(c => c.GetDependencyMap(typeof(Animal).GetDependencyId())).Returns(fakeConDict[typeof(Animal).GetDependencyId()]);
            mockDIContainer.Setup(c => c.GetDependencyMap(typeof(Monkey).GetDependencyId())).Returns(fakeConDict[typeof(Monkey).GetDependencyId()]);
            mockDIContainer.Setup(c => c.GetDependencyMap(typeof(Donkey).GetDependencyId())).Returns(fakeConDict[typeof(Donkey).GetDependencyId()]);
            mockDIContainer.Setup(c => c.GetDependencyMap(typeof(Dog).GetDependencyId())).Returns(fakeConDict[typeof(Dog).GetDependencyId()]);

            mockCanBeInjectableRegistrar = new Mock<CanBeInjectedDependencyRegistrar>(mockDIContainer.Object) { CallBase = true };
        }

        [TestMethod]
        public void CanBeInjectedDependencyRegistrar_Register_Same_Types_Test()
        {
            //arrage
            Type[] types = new Type[] { typeof(Monkey) };

            //act
            mockCanBeInjectableRegistrar.Object.Register(types);

            //assert
            mockDIContainer.Verify(c => c.GetDependencyMap(It.IsAny<string>()), Times.Never());
            mockCanBeInjectableRegistrar.Verify(idr => idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
            Assert.IsTrue(mockDIContainer.Object.GetDependencyMap(typeof(Monkey).GetDependencyId()).PrimaryDependencyHolder.DependencyId== typeof(Monkey).GetDependencyId());
            Assert.IsTrue(mockDIContainer.Object.GetDependencyMap(typeof(Monkey).GetDependencyId()).SecondaryDependencyHolder.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NonInjectableTypeException))]
        public void CanBeInjectedDependencyRegistrar_Register_NonInjectable_Target_Test()
        {
            //arrage
            IDependencyMap dependencyMap;
            fakeConDict.TryRemove(typeof(Donkey).GetDependencyId(), out dependencyMap);
            Type[] types = new Type[] { typeof(Donkey) };

            //act
            mockCanBeInjectableRegistrar.Object.Register(types);

            //assert

        }

        [TestMethod]
        [ExpectedException(typeof(NonInjectableTypeException))]
        public void CanBeInjectedDependencyRegistrar_Register_NonInjectable_Source_Test()
        {
            //arrage
            IDependencyMap dependencyMap;
            fakeConDict.TryRemove(typeof(Animal).GetDependencyId(), out dependencyMap);
            Type[] types = new Type[] { typeof(Donkey) };

            //act
            mockCanBeInjectableRegistrar.Object.Register(types);

            //assert
        }

        [TestMethod]
        public void CanBeInjectedDependencyRegistrar_Dont_Register_Non_Injectable_Test()
        {
            //arrage
            Type[] types = new Type[] { typeof(foo) };

            //act
            mockCanBeInjectableRegistrar.Object.Register(types);

            //assert
            mockDIContainer.Verify(c => c.GetDependencyMap(It.IsAny<string>()), Times.Never());
            mockCanBeInjectableRegistrar.Verify(idr => idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
        }

        [TestMethod]
        public void CanBeInjectedDependencyRegistrar_Target_Types_Test()
        {
            //arrage
            Type[] types = new Type[] { typeof(Donkey),typeof(Animal) };

            //act
            mockCanBeInjectableRegistrar.Object.Register(types);

            //assert
            mockCanBeInjectableRegistrar.Verify(idr => idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
            Assert.IsTrue(mockDIContainer.Object.GetDependencyMap(typeof(Animal).GetDependencyId()).SecondaryDependencyHolder.Count == 1);
        }

        [TestMethod]
        public void CanBeInjectedDependencyRegistrar_Two_Target_Types_Test()
        {
            //arrage
            Type[] types = new Type[] { typeof(Donkey), typeof(Dog), typeof(Animal) };

            //act
            mockCanBeInjectableRegistrar.Object.Register(types);

            //assert
            mockCanBeInjectableRegistrar.Verify(idr => idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
            Assert.IsTrue(mockDIContainer.Object.GetDependencyMap(typeof(Animal).GetDependencyId()).SecondaryDependencyHolder.Count==2);
        }
    }
}