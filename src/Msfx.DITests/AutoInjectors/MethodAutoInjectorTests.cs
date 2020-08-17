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
    public class MethodAutoInjectorTests
    {
        [TestMethod]
        public void MethodAutoInjector_Inject_Test()
        {
            // arrange
            Mock<IDependencyHolder> mockDependencyHolder = new Mock<IDependencyHolder>();
            mockDependencyHolder.Setup(dh => dh.GetInstance(null)).Returns(new bar());

            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();
            mockDependencyMap.Setup(dm => dm.PrimaryDependencyHolder).Returns(mockDependencyHolder.Object);

            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(true);
            mockContainer.Setup(c => c.GetDependencyMap(It.IsAny<string>())).Returns(mockDependencyMap.Object);

            Mock<MethodAutoInjector> mockMethodAutoInjector = new Mock<MethodAutoInjector>(mockContainer.Object, typeof(foo)) { CallBase = true };
            mockMethodAutoInjector.Setup(pa => pa.GetParamInjectDependency(It.IsAny<ParameterInfo>())).Returns((string)null);

            foo afoo = new foo();

            //act
            mockMethodAutoInjector.Object.Inject(afoo);

            //assert
            Assert.IsNotNull(afoo.Get_pbar());
        }

        [TestMethod]
        [ExpectedException(typeof(PrimaryOrPreferredTargetDependencyNotFound))]
        public void MethodAutoInjector_Inject_PrimaryOrPreferredTargetDependencyNotFound_Test()
        {
            // arrange
            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();
            mockDependencyMap.Setup(dm => dm.PrimaryDependencyHolder).Returns((IDependencyHolder)null);

            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(true);
            mockContainer.Setup(c => c.GetDependencyMap(It.IsAny<string>())).Returns(mockDependencyMap.Object);

            Mock<MethodAutoInjector> mockMethodAutoInjector = new Mock<MethodAutoInjector>(mockContainer.Object, typeof(foo)) { CallBase = true };
            mockMethodAutoInjector.Setup(pa => pa.GetParamInjectDependency(It.IsAny<ParameterInfo>())).Returns((string)null);

            foo afoo = new foo();

            //act
            mockMethodAutoInjector.Object.Inject(afoo);

            //assert
            Assert.IsNull(afoo.Get_bar());
        }


        [TestMethod]
        [ExpectedException(typeof(NonInjectableTypeException))]
        public void MethodAutoInjector_NonInjectableTypeException_Test()
        {
            // arrange
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(false);

            Mock<MethodAutoInjector> mockMethodAutoInjector = new Mock<MethodAutoInjector>(mockContainer.Object, typeof(foo)) { CallBase = true };
            mockMethodAutoInjector.Setup(pa => pa.GetParamInjectDependency(It.IsAny<ParameterInfo>())).Returns((string)null);

            foo afoo = new foo();

            //act
            mockMethodAutoInjector.Object.Inject(afoo);

            //assert
            Assert.IsNull(afoo.Get_bar());
        }
    }
}