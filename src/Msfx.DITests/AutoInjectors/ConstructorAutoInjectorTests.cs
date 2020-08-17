using FakeTypes.For.AutoInjectors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.AutoInjectors;
using Msfx.DI.Containers;
using Msfx.DI.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.AutoInjectors.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ConstructorAutoInjectorTests
    {
        [TestMethod]
        public void ConstructorAutoInjector_Inject_Test()
        {
            //arrage
            Mock<IDependencyHolder> mockDependencyHolder = new Mock<IDependencyHolder>();
            mockDependencyHolder.Setup(dh => dh.GetInstance(null)).Returns(new bar());

            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();
            mockDependencyMap.Setup(dm => dm.PrimaryDependencyHolder).Returns(mockDependencyHolder.Object);

            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(true);
            mockContainer.Setup(c => c.GetDependencyMap(It.IsAny<string>())).Returns(mockDependencyMap.Object);

            Mock<ConstructorAutoInjector> mockConstructorAutoInjector = new Mock<ConstructorAutoInjector>(mockContainer.Object, typeof(foo)) { CallBase = true };
            mockConstructorAutoInjector.Setup(pa => pa.GetParamInjectDependency(It.IsAny<ParameterInfo>())).Returns((string)null);

            foo afoo = new foo();

            //act
            mockConstructorAutoInjector.Object.Inject(afoo);

            //assert
            Assert.IsNotNull(afoo.Get_bar());
        }
        [TestMethod]
        [ExpectedException(typeof(PrimaryOrPreferredTargetDependencyNotFound))]
        public void ConstructorAutoInjector_Inject_PrimaryOrPreferredTargetDependencyNotFound_Test()
        {
            //arrage
            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();
            mockDependencyMap.Setup(dm => dm.PrimaryDependencyHolder).Returns((IDependencyHolder)null);

            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(true);
            mockContainer.Setup(c => c.GetDependencyMap(It.IsAny<string>())).Returns(mockDependencyMap.Object);

            Mock<ConstructorAutoInjector> mockConstructorAutoInjector = new Mock<ConstructorAutoInjector>(mockContainer.Object, typeof(foo)) { CallBase = true };
            mockConstructorAutoInjector.Setup(pa => pa.GetParamInjectDependency(It.IsAny<ParameterInfo>())).Returns((string)null);

            foo afoo = new foo();

            //act
            mockConstructorAutoInjector.Object.Inject(afoo);

            //assert
            Assert.IsNull(afoo.Get_bar());
        }

        [TestMethod]
        [ExpectedException(typeof(NonInjectableTypeException))]
        public void ConstructorAutoInjector_Inject_NonInjectableTypeException_Test()
        {
            //arrage
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(false);

            Mock<ConstructorAutoInjector> mockConstructorAutoInjector = new Mock<ConstructorAutoInjector>(mockContainer.Object, typeof(foo)) { CallBase = true };
            mockConstructorAutoInjector.Setup(pa => pa.GetParamInjectDependency(It.IsAny<ParameterInfo>())).Returns((string)null);

            foo afoo = new foo();

            //act
            mockConstructorAutoInjector.Object.Inject(afoo);

            //assert
            Assert.IsNull(afoo.Get_bar());
        }

        [TestMethod]
        public void ConstructorAutoInjector_Ctor2_Test()
        {
            //arrage
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            Mock<MemberAutoInjector> mockMemberAutoInjector = new Mock<MemberAutoInjector>(It.IsAny<IDIContainer>(), It.IsAny<Type>());

            ConstructorAutoInjector constructorAutoInjector;

            //act
            constructorAutoInjector = new ConstructorAutoInjector(mockMemberAutoInjector.Object, mockContainer.Object, typeof(foo));

            //assert
            Assert.AreSame(constructorAutoInjector.Successor, mockMemberAutoInjector.Object);
            Assert.IsNotNull(constructorAutoInjector.Container);
        }
    }
}