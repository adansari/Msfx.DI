using FakeTypes.For.InjectForTypeDependencyRegistrarTests;
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
    public class InjectForTypeDependencyRegistrarTests
    {
        ConcurrentDictionary<string, IDependencyMap> fakeConDict;
        Mock<ConcurrentDictDIContainer> mockDIContainer;
        Mock<InjectForTypeDependencyRegistrar> mockInjectForTypeRegistrar;

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

            mockInjectForTypeRegistrar = new Mock<InjectForTypeDependencyRegistrar>(mockDIContainer.Object) { CallBase = true };
        }

        [TestMethod]
        public void InjectForTypeDependencyRegistrar_Register_Same_Types_Test()
        {
            //arrage
            Type[] types = new Type[] { typeof(Monkey)};

            //act
            mockInjectForTypeRegistrar.Object.Register(types);

            //assert
            mockDIContainer.Verify(c => c.GetDependencyMap(It.IsAny<string>()), Times.Never());
            mockInjectForTypeRegistrar.Verify(idr => idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
            Assert.IsTrue(mockDIContainer.Object.GetDependencyMap(typeof(Monkey).GetDependencyId()).PrimaryDependencyHolder.DependencyId == typeof(Monkey).GetDependencyId());
        }

        [TestMethod]
        [ExpectedException(typeof(NonInjectableTypeException))]
        public void InjectForTypeDependencyRegistrar_Register_NonInjectable_Target_Test()
        {
            //arrage
            IDependencyMap dependencyMap;
            fakeConDict.TryRemove(typeof(Donkey).GetDependencyId(), out dependencyMap);
            Type[] types = new Type[] { typeof(Donkey) };

            //act
            mockInjectForTypeRegistrar.Object.Register(types);

            //assert

        }

        [TestMethod]
        [ExpectedException(typeof(NonInjectableTypeException))]
        public void InjectForTypeDependencyRegistrar_Register_NonInjectable_Source_Test()
        {
            //arrage
            IDependencyMap dependencyMap;
            fakeConDict.TryRemove(typeof(Animal).GetDependencyId(), out dependencyMap);
            Type[] types = new Type[] {typeof(Donkey) };

            //act
            mockInjectForTypeRegistrar.Object.Register(types);

            //assert
        }

        [TestMethod]
        public void InjectForTypeDependencyRegistrar_Dont_Register_Test()
        {
            //arrage
            Type[] types = new Type[] { typeof(foo) };

            //act
            mockInjectForTypeRegistrar.Object.Register(types);

            //assert
            mockDIContainer.Verify(c => c.GetDependencyMap(It.IsAny<string>()), Times.Never());
            mockInjectForTypeRegistrar.Verify(idr => idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
        }

        [TestMethod]
        public void InjectForTypeDependencyRegistrar_Target_Types_Test()
        {
            //arrage
            Type[] types = new Type[] { typeof(Donkey), typeof(Animal) };

            //act
            mockInjectForTypeRegistrar.Object.Register(types);

            //assert
            mockInjectForTypeRegistrar.Verify(idr => idr.RegisterNext(It.IsAny<IEnumerable<Type>>()));
            Assert.IsTrue(mockDIContainer.Object.GetDependencyMap(typeof(Animal).GetDependencyId()).PrimaryDependencyHolder.DependencyId == typeof(Donkey).GetDependencyId());
        }

        [TestMethod]
        [ExpectedException(typeof(InjectionAmbiguityException))]
        public void InjectForTypeDependencyRegistrar_Two_Target_Types_Test()
        {
            //arrage
            Type[] types = new Type[] { typeof(Donkey), typeof(Dog), typeof(Animal) };

            //act
            mockInjectForTypeRegistrar.Object.Register(types);

            //assert
        }
    }
}