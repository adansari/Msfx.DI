using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Containers;
using System;
using System.Diagnostics.CodeAnalysis;
using FakeTypes.For.AutoInjectors;
using System.Reflection;

namespace Msfx.DI.AutoInjectors.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class MemberAutoInjectorTests
    {
        [TestMethod]
        public void MemberAutoInjector_GetMemberInjectDependency_Test()
        {
            //arrage
            string expectedDependency = "FakeTypes.For.AutoInjectors.bar";
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase= true};

            //act
            string actualDependency = mockAutoInjector.Object.GetMemberInjectDependency(typeof(foo).GetProperty("PropBar"));

            //assert
            Assert.AreEqual(expectedDependency, actualDependency);
        }

        [TestMethod]
        public void MemberAutoInjector_GetMemberInjectDependency_Null_Test()
        {
            //arrage
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            string actualDependency = mockAutoInjector.Object.GetMemberInjectDependency(typeof(foo).GetMember("PropFoo")[0]);

            //assert
            Assert.IsNull(actualDependency);
        }

        [TestMethod]
        public void MemberAutoInjector_GetParamInjectDependency_Test()
        {
            //arrage
            string expectedDependency = "FakeTypes.For.AutoInjectors.bar";
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            string actualDependency = mockAutoInjector.Object.GetParamInjectDependency(typeof(foo).GetMethod("DoFoo").GetParameters()[1]);

            //assert
            Assert.AreEqual(expectedDependency, actualDependency);
        }

        [TestMethod]
        public void MemberAutoInjector_GetParamInjectDependency_Null_Test()
        {
            //arrage
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            string actualDependency = mockAutoInjector.Object.GetParamInjectDependency(typeof(foo).GetMethod("DoFoo").GetParameters()[0]);

            //assert
            Assert.IsNull(actualDependency);
        }

        [TestMethod]
        public void MemberAutoInjector_GetParamDefaultValues_Test()
        {
            //arrage
            int expectedInjectValue = 10;
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            var actualInjectValue = mockAutoInjector.Object.GetParamInjectValues(typeof(foo).GetMethod("DoPow").GetParameters()[0]);

            //assert
            Assert.AreEqual(expectedInjectValue, (int)actualInjectValue[0]);
        }

        [TestMethod]
        public void MemberAutoInjector_GetParamDefaultValues_Null_Test()
        {
            //arrage
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            var actualInjectValue = mockAutoInjector.Object.GetParamInjectValues(typeof(foo).GetConstructors()[1].GetParameters()[0]);

            //assert
            Assert.IsNull(actualInjectValue);
        }

        [TestMethod]
        public void MemberAutoInjector_GetMemberInjectValues_Test()
        {
            //arrage
            int expectedInjectValue = 20;
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            var actualInjectValue = mockAutoInjector.Object.GetMemberInjectValues(typeof(foo).GetProperty("PropPow"));

            //assert
            Assert.AreEqual(expectedInjectValue, (int)actualInjectValue[0]);
        }

        [TestMethod]
        public void MemberAutoInjector_GetMemberInjectValues_Null_Test()
        {
            //arrage
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            var actualInjectValue = mockAutoInjector.Object.GetMemberInjectValues(typeof(foo).GetProperty("PropBar"));

            //assert
            Assert.IsNull(actualInjectValue);
        }

        [TestMethod]
        public void MemberAutoInjector_Get_Container_Test()
        {
            //arrage
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            Mock<MemberAutoInjector> mockAutoInjector = new Mock<MemberAutoInjector>(mockContainer.Object, It.IsAny<Type>()) { CallBase = true };

            //act
            IDIContainer actualContainer = mockAutoInjector.Object.Container;

            //assert
            Assert.AreSame(mockContainer.Object, actualContainer);
        }
    }
}