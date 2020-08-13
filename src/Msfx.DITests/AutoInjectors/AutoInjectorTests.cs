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
    public class AutoInjectorTests
    {
        [TestMethod]
        public void AutoInjector_GetMemberPreferredDependency_Test()
        {
            //arrage
            string expectedDependency = "FakeTypes.For.AutoInjectors.bar";
            Mock<AutoInjector> mockAutoInjector = new Mock<AutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase= true};

            //act
            string actualDependency = mockAutoInjector.Object.GetMemberPreferredDependency(typeof(foo).GetMember("Bar")[0]);

            //assert
            Assert.AreEqual(expectedDependency, actualDependency);
        }

        [TestMethod]
        public void AutoInjector_GetMemberPreferredDependency_Null_Test()
        {
            //arrage
            Mock<AutoInjector> mockAutoInjector = new Mock<AutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            string actualDependency = mockAutoInjector.Object.GetMemberPreferredDependency(typeof(foo).GetMember("Foo")[0]);

            //assert
            Assert.IsNull(actualDependency);
        }

        [TestMethod]
        public void AutoInjector_GetParamPreferredDependency_Test()
        {
            //arrage
            string expectedDependency = "FakeTypes.For.AutoInjectors.bar";
            Mock<AutoInjector> mockAutoInjector = new Mock<AutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            string actualDependency = mockAutoInjector.Object.GetParamPreferredDependency(typeof(foo).GetMethod("DoFoo").GetParameters()[1]);

            //assert
            Assert.AreEqual(expectedDependency, actualDependency);
        }

        [TestMethod]
        public void AutoInjector_GetParamPreferredDependency_Null_Test()
        {
            //arrage
            Mock<AutoInjector> mockAutoInjector = new Mock<AutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>()) { CallBase = true };

            //act
            string actualDependency = mockAutoInjector.Object.GetParamPreferredDependency(typeof(foo).GetMethod("DoFoo").GetParameters()[0]);

            //assert
            Assert.IsNull(actualDependency);
        }

        [TestMethod]
        public void AutoInjector_Get_Container_Test()
        {
            //arrage
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            Mock<AutoInjector> mockAutoInjector = new Mock<AutoInjector>(mockContainer.Object, It.IsAny<Type>()) { CallBase = true };

            //act
            IDIContainer actualContainer = mockAutoInjector.Object.Container;

            //assert
            Assert.AreSame(mockContainer.Object, actualContainer);
        }
    }
}