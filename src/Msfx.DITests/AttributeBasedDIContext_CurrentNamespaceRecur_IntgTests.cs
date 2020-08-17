using FakeTypes.For.DITests.NamespaceRecur;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msfx.DI.Scanners;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AttributeBasedDIContext_CurrentNamespaceRecur_IntgTests
    {
        DIContext diCtx;

        [TestInitialize]
        public void Init()
        {
            diCtx = new AttributeBasedDIContext(typeof(FakeTypes.For.DITests.Boot),DependencyScanTarget.CurrentNamespaceRecursive);
        }

        [TestMethod]
        public void CurrentNamespaceRecur_Class_Injection_Ctor_With_Param_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            FakeTypes.For.DITests.foo afoo = diCtx.Inject<FakeTypes.For.DITests.foo>("prop value");

            //assert
            Assert.IsNotNull(afoo);
            Assert.AreEqual("prop value",afoo.strProp);
        }

        [TestMethod]
        public void CurrentNamespaceRecur_Class_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            Dog dog= diCtx.Inject<Dog>();

            //assert
            Assert.IsInstanceOfType(dog, typeof(Dog));
        }

        [TestMethod]
        public void CurrentNamespaceRecur_Abstract_Class_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            Animal animal = diCtx.Inject<Animal>();

            //assert
            Assert.IsInstanceOfType(animal, typeof(Dog));
        }

        [TestMethod]
        public void CurrentNamespaceRecur_Interface_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            IMammal mammal = diCtx.Inject<IMammal>();

            //assert
            Assert.IsInstanceOfType(mammal, typeof(Cat));
        }

        [TestMethod]
        public void CurrentNamespaceRecur_Abstract_Class_Target_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            Animal animal = diCtx.Inject<Animal, Dog>();

            //assert
            Assert.IsInstanceOfType(animal, typeof(Dog));
        }

        [TestMethod]
        public void CurrentNamespaceRecur_Interface_Target_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            IMammal mammal = diCtx.Inject<IMammal,Dog>();

            //assert
            Assert.IsInstanceOfType(mammal, typeof(Dog));
        }

    }
}
