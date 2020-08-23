using FakeAssembly;
using FakeTypes.For.DITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msfx.DI.Containers;
using Msfx.DI.Exceptions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DIContextTests
    {
        Mock<TestAssembly> mockAssembly;

        [TestInitialize]
        public void Init()
        {
            mockAssembly = MockAssembly.GetMockWithTypeSetup(NamesapceSetupLevel.DI);
        }

        [TestMethod]
        public void DIContext_Constructor_With_Param_Type_Test()
        {
            //arrange
            DIContext diCtx;
            string expectedCurrentNamespace = "FakeTypes.For.DITests";

            //act
            diCtx = new AttributeBasedDIContext(typeof(Boot));

            //assert
            Assert.AreEqual(expectedCurrentNamespace, diCtx.CurrentNamespace);
            Assert.AreEqual(mockAssembly.Object.FullName, diCtx.CallingAssembly.FullName);
        }

        [TestMethod]
        public void DIContext_Get_Container_Test()
        {
            //arrange
            Mock<DIContext> mockDIContext;

            //act
            mockDIContext = new Mock<DIContext>() { CallBase = true };

            //assert
            Assert.IsNotNull(mockDIContext.Object.Container);
        }


        [TestMethod]
        public void DIContext_Inject_Non_Injectable_Test()
        {
            //arrange
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(false);

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            _foo obj = mockDIContext.Object.Inject<_foo>();

            //assert
            Assert.IsNull(obj);
        }

        [TestMethod]
        public void DIContext_Inject_Test()
        {
            //arrange
            Mock<IDependencyHolder> mockDependencyHolder = new Mock<IDependencyHolder>();
            mockDependencyHolder.Setup(dh => dh.GetInstance(It.IsAny<object[]>())).Returns(new foo());

            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();
            mockDependencyMap.Setup(dm => dm.PrimaryDependencyHolder).Returns(mockDependencyHolder.Object);

            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(true);
            mockContainer.Setup(c => c.GetDependencyMap(It.IsAny<string>())).Returns(mockDependencyMap.Object);

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            foo objFoo = mockDIContext.Object.Inject<foo>();

            //assert
            mockContainer.Verify(c => c.ContainsDependency(It.IsAny<string>()));
            mockContainer.Verify(c => c.GetDependencyMap(It.IsAny<string>()));
            mockDependencyMap.Verify(dm => dm.PrimaryDependencyHolder);
            mockDependencyHolder.Verify(dh => dh.GetInstance(It.IsAny<object[]>()));
            Assert.IsInstanceOfType(objFoo, typeof(foo));
            Assert.IsNotNull(objFoo);

        }

        [TestMethod]
        public void DIContext_Inject2_Same_Source_Target_Test()
        {
            //arrange
            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.Setup(ctx => ctx.Inject<foo>(It.IsAny<object[]>())).Returns(new foo());

            //act
            foo objfoo = mockDIContext.Object.Inject<foo, foo>();

            //assert
            mockDIContext.Verify(ctx => ctx.Inject<foo>(It.IsAny<object[]>()), Times.Exactly(1));
            Assert.IsInstanceOfType(objfoo, typeof(foo));
            Assert.IsNotNull(objfoo);
        }

        [TestMethod]
        public void DIContext_Inject2_Non_Injectable_Source_Test()
        {
            //arrange
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(false);

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            Ifoo objIfoo = mockDIContext.Object.Inject<Ifoo, foo>();

            //assert
            Assert.IsNull(objIfoo);
        }

        [TestMethod]
        public void DIContext_Inject2_Target_Is_Primary_Dependency_Holder_Test()
        {
            //arrange
            Mock<IDependencyHolder> mockDependencyHolder = new Mock<IDependencyHolder>();
            mockDependencyHolder.Setup(dh => dh.GetInstance(It.IsAny<object[]>())).Returns(new Cat());
            mockDependencyHolder.SetupGet(dh => dh.DependencyId).Returns(typeof(Cat).GetDependencyId());

            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();
            mockDependencyMap.Setup(dm => dm.PrimaryDependencyHolder).Returns(mockDependencyHolder.Object);
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(true);
            mockContainer.Setup(c => c.GetDependencyMap(It.IsAny<string>())).Returns(mockDependencyMap.Object);

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.Setup(ctx => ctx.Inject<Animal>(It.IsAny<object[]>())).Returns(new Cat());
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            Animal cat = mockDIContext.Object.Inject<Animal, Cat>();

            //assert
            mockDIContext.Verify(ctx => ctx.Inject<Animal>(It.IsAny<object[]>()), Times.Exactly(1));
            Assert.IsInstanceOfType(cat, typeof(Cat));
            Assert.IsNotNull(cat);
        }

        [TestMethod]
        public void DIContext_Inject2_Target_Is_Secondary_Dependency_Holder_Test()
        {
            //arrange
            Mock<IDependencyHolder> mockDependencyHolder = new Mock<IDependencyHolder>();
            mockDependencyHolder.Setup(dh => dh.GetInstance(It.IsAny<object[]>())).Returns(new Cat());

            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();
            mockDependencyMap.Setup(dm => dm.GetSecondaryDependencyHolder(It.IsAny<string>())).Returns(mockDependencyHolder.Object);

            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(true);
            mockContainer.Setup(c => c.GetDependencyMap(It.IsAny<string>())).Returns(mockDependencyMap.Object);

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            Animal cat = mockDIContext.Object.Inject<Animal, Cat>();

            //assert
            Assert.IsInstanceOfType(cat, typeof(Cat));
            Assert.IsNotNull(cat);
        }

        [TestMethod]
        public void DIContext_Inject2_Secondary_Dependency_Holder_Is_Null_Test()
        {
            //arrange
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.ContainsDependency(It.IsAny<string>())).Returns(true);
            mockContainer.Setup(c => c.GetDependencyMap(It.IsAny<string>())).Returns(new Mock<IDependencyMap>().Object);

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            Animal cat = mockDIContext.Object.Inject<Animal, Cat>();

            //assert
            Assert.IsNull(cat);
        }

        [TestMethod]
        public void DIContext_InjectByName_Test()
        {
            //arrange
            Mock<IDependencyHolder> mockDependencyHolder = new Mock<IDependencyHolder>();
            mockDependencyHolder.Setup(dh => dh.GetInstance(It.IsAny<object[]>())).Returns(new foo());

            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();
            mockDependencyMap.Setup(dm => dm.PrimaryDependencyHolder).Returns(mockDependencyHolder.Object);

            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.SearchDependency(It.IsAny<string>())).Returns(new List<IDependencyMap>() { mockDependencyMap.Object });

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            foo afoo = mockDIContext.Object.InjectByName<foo>("foo");

            //assert
            Assert.IsInstanceOfType(afoo, typeof(foo));
        }

        [TestMethod]
        public void DIContext_InjectByName_Nonclass_Test()
        {
            //arrange

            Mock<IDependencyMap> mockDependencyMap = new Mock<IDependencyMap>();

            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.SearchDependency(It.IsAny<string>())).Returns(new List<IDependencyMap>() { mockDependencyMap.Object });

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            Ifoo afoo = mockDIContext.Object.InjectByName<Ifoo>("Ifoo");

            //assert
            Assert.IsNull(afoo);
        }

        [TestMethod]
        [ExpectedException(typeof(NonInjectableTypeException))]
        public void DIContext_InjectByName_TypeNotFound_Test()
        {
            //arrange
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.SearchDependency(It.IsAny<string>())).Returns(new List<IDependencyMap>());

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            foo foo = mockDIContext.Object.InjectByName<foo>("NotExists");

            //assert
            Assert.IsNull(foo);
        }

        [TestMethod]
        [ExpectedException(typeof(InjectionAmbiguityException))]
        public void DIContext_InjectByName_MultipleTypeFound_Test()
        {
            //arrange
            Mock<IDependencyMap> mockDependencyMap1 = new Mock<IDependencyMap>();
            Mock<IDependencyMap> mockDependencyMap2 = new Mock<IDependencyMap>();
            Mock<IDIContainer> mockContainer = new Mock<IDIContainer>();
            mockContainer.Setup(c => c.SearchDependency(It.IsAny<string>())).Returns(new List<IDependencyMap>() { mockDependencyMap1.Object, mockDependencyMap2.Object });

            Mock<DIContext> mockDIContext = new Mock<DIContext>(typeof(Boot)) { CallBase = true };
            mockDIContext.SetupGet(ctx => ctx.Container).Returns(mockContainer.Object);

            //act
            foo foo = mockDIContext.Object.InjectByName<foo>("NotExists");

            //assert 
            Assert.IsNull(foo);
        }

        [TestMethod]
        public void DIContext_Failing_Test()
        {
            Assert.Fail("falied!!!");
        }
    }
}
