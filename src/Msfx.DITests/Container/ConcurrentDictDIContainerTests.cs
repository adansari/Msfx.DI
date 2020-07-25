using FakeTypes.For.NonDITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.LifeTimeManagers;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Containers.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ConcurrentDictDIContainerTests
    {
        Type fakeType;
        string fakeDependencyId, fakeDependencyId2;
        IDependencyMap fakeDependencyMap;
        ConcurrentDictionary<string, IDependencyMap> fakeDict;
        Mock<ConcurrentDictDIContainer> mockDIContainer;
        

        [TestInitialize]
        public void Init()
        {
            fakeType = typeof(foo);
            fakeDependencyId = fakeType.GetDependencyId();
            fakeDependencyId2 = typeof(Ifoo).GetDependencyId();
            fakeDependencyMap = new DependencyMap(fakeType);

            fakeDict = new ConcurrentDictionary<string, IDependencyMap>();
            fakeDict.TryAdd(fakeType.GetDependencyId(), fakeDependencyMap);

            mockDIContainer = new Mock<ConcurrentDictDIContainer>() { CallBase = true };
            mockDIContainer.SetupGet(dict => dict.Container).Returns(fakeDict);
        }

        [TestMethod]
        public void ConcurrentDictDIContainer_Container_Get_Test()
        {
            //arrange
            ConcurrentDictDIContainer container;

            //act
            container = new ConcurrentDictDIContainer();

            //Assert
            Assert.IsNotNull(container.Container);
        }

        [TestMethod]
        public void ConcurrentDictDIContainer_Indexer_Test()
        {
            //arrange
            IDependencyMap expectedDependencyMap = fakeDependencyMap;
            IDependencyMap actualDependencyMap;
            mockDIContainer.Object.Register(fakeDependencyId, fakeDependencyMap);

            //act
            actualDependencyMap = mockDIContainer.Object[fakeDependencyId];

            //assert
            Assert.AreSame(expectedDependencyMap, actualDependencyMap);
            Assert.IsNull(mockDIContainer.Object.GetDependencyMap(fakeDependencyId2));
        }

        [TestMethod]
        public void ConcurrentDictDIContainer_ContainsDependency_Test()
        {
            //arrange
            bool actualTrue, actualFalse;

            //act
            actualTrue = mockDIContainer.Object.ContainsDependency(fakeDependencyId);
            actualFalse = mockDIContainer.Object.ContainsDependency(fakeDependencyId2);

            //Assert
            Assert.AreEqual(true, actualTrue);
            Assert.AreEqual(false, actualFalse);
        }

        [TestMethod]
        public void ConcurrentDictDIContainer_GetDependencyMap_Test()
        {
            //arrange
            IDependencyMap expectedDependencyMap = fakeDependencyMap;
            IDependencyMap actualDependencyMap;
            mockDIContainer.Object.Register(fakeDependencyId, fakeDependencyMap);

            //act
            actualDependencyMap = mockDIContainer.Object.GetDependencyMap(fakeDependencyId);

            //assert
            Assert.AreSame(expectedDependencyMap, actualDependencyMap);
            Assert.IsNull(mockDIContainer.Object.GetDependencyMap(fakeDependencyId2));
        }

        [TestMethod]
        public void ConcurrentDictDIContainer_Register_Test()
        {
            //arrange
            string expectedDependencyId = fakeDependencyId;

            //act
            mockDIContainer.Object.Register(fakeDependencyId, fakeType, InstanceType.Local);

            //assert
            Assert.AreEqual(expectedDependencyId, mockDIContainer.Object.GetDependencyMap(fakeDependencyId).SourceDependencyId);
            Assert.IsNull(mockDIContainer.Object.GetDependencyMap(fakeDependencyId2));
        }

        [TestMethod]
        public void ConcurrentDictDIContainer_Register2_Test()
        {
            //arrange
            string expectedDependencyId = fakeDependencyId;

            //act
            mockDIContainer.Object.Register(fakeDependencyId, fakeDependencyMap);

            //assert
            Assert.AreEqual(expectedDependencyId, mockDIContainer.Object.GetDependencyMap(fakeDependencyId).SourceDependencyId);
            Assert.IsNull(mockDIContainer.Object.GetDependencyMap(fakeDependencyId2));
        }
    }
}