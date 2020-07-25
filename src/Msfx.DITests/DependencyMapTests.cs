using FakeTypes.For.DITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.LifeTimeManagers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DependencyMapTests
    {
        [TestMethod]
        public void DependencyMap_Ctor_Test()
        {
            //arrange
            Type type = typeof(foo);
            IDependencyMap dependencyMap;

            //act
            dependencyMap = new DependencyMap(type);

            //assert
            Assert.AreEqual(type.GetDependencyId(), dependencyMap.SourceDependencyId);
            Assert.AreEqual(false, dependencyMap.IsAbstractOrInterface);
            Assert.IsNotNull(dependencyMap.PrimaryDependencyHolder);
        }

        [TestMethod]
        public void DependencyMap_Ctor_Interface_Test()
        {
            //arrange
            Type type = typeof(Ifoo);
            IDependencyMap dependencyMap;

            //act
            dependencyMap = new DependencyMap(type);

            //assert
            Assert.AreEqual(type.GetDependencyId(), dependencyMap.SourceDependencyId);
            Assert.AreEqual(true, dependencyMap.IsAbstractOrInterface);
            Assert.IsNull(dependencyMap.PrimaryDependencyHolder);
        }

        [TestMethod]
        public void DependencyMap_Ctor2_Test()
        {
            //arrange
            Type type = typeof(foo);
            IDependencyMap dependencyMap;

            //act
            dependencyMap = new DependencyMap(type,InstanceType.Local);

            //assert
            Assert.AreEqual(type.GetDependencyId(), dependencyMap.SourceDependencyId);
            Assert.AreEqual(false, dependencyMap.IsAbstractOrInterface);
            Assert.IsNotNull(dependencyMap.PrimaryDependencyHolder);
        }

        [TestMethod]
        public void DependencyMap_Ctor2_Asbtract_Test()
        {
            //arrange
            Type type = typeof(abs_foo);
            IDependencyMap dependencyMap;

            //act
            dependencyMap = new DependencyMap(type, InstanceType.Local);

            //assert
            Assert.AreEqual(type.GetDependencyId(), dependencyMap.SourceDependencyId);
            Assert.AreEqual(true, dependencyMap.IsAbstractOrInterface);
            Assert.IsNull(dependencyMap.PrimaryDependencyHolder);
        }

        [TestMethod]
        public void DependencyMap_GetSecondaryDependencyHolder_Test()
        {
            //arrange
            Type type = typeof(foo);
            Mock<IDependencyHolder> mockDepHolder = new Mock<IDependencyHolder>();
            mockDepHolder.SetupGet(dh => dh.DependencyId).Returns(type.GetDependencyId());

            IDependencyMap dependencyMap = new DependencyMap(type);
            dependencyMap.SecondaryDependencyHolder.Add(mockDepHolder.Object);

            //act
            IDependencyHolder actualDepHolder = dependencyMap.GetSecondaryDependencyHolder(type.GetDependencyId());

            //assert
            Assert.IsNotNull(actualDepHolder);
        }
    }
}