using FakeTypes.For.DITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AttributeBasedDIContext_CurrentNamespace_IntgTests
    {
        DIContext diCtx;

        [TestInitialize]
        public void Init()
        {
            diCtx = new AttributeBasedDIContext(typeof(Boot));
        }

        [TestMethod]
        public void CurrentNamespace_Class_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            foo objfoo = diCtx.Inject<foo>();

            //assert
            Assert.IsInstanceOfType(objfoo, typeof(foo));
        }

        [TestMethod]
        public void CurrentNamespace_Abstract_Class_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            Animal animal = diCtx.Inject<Animal>();

            //assert
            Assert.IsInstanceOfType(animal, typeof(Dog));
        }

        [TestMethod]
        public void CurrentNamespace_Interface_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            IMammal mammal = diCtx.Inject<IMammal>();

            //assert
            Assert.IsInstanceOfType(mammal, typeof(Cat));
        }

        [TestMethod]
        public void CurrentNamespace_Abstract_Class_Target_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            Animal animal = diCtx.Inject<Animal, Dog>();

            //assert
            Assert.IsInstanceOfType(animal, typeof(Dog));
        }

        [TestMethod]
        public void CurrentNamespace_Interface_Target_Injection_IntgTests()
        {
            //arrange
            diCtx.Scan();

            //act
            IMammal mammal = diCtx.Inject<IMammal,Dog>();

            //assert
            Assert.IsInstanceOfType(mammal, typeof(Dog));
        }

        [TestMethod]
        public void CurrentNamespace_Class_Injection_Static_Type_IntgTests()
        {
            //arrange
            diCtx.Scan();
            foo objfoo = diCtx.Inject<foo>();
            objfoo.strProp = "state changed";

            //act
            foo anotherObjfoo = diCtx.Inject<foo>();

            //assert
            Assert.AreSame(objfoo, anotherObjfoo);
            Assert.AreSame(objfoo.strProp, anotherObjfoo.strProp);
        }

        [TestMethod]
        public void CurrentNamespace_Class_Injection_Local_Type_IntgTests()
        {
            //arrange
            diCtx.Scan();
            fooLocal objfoo = diCtx.Inject<fooLocal>();
            objfoo.strProp = "state changed";

            //act
            fooLocal anotherObjfoo = diCtx.Inject<fooLocal>();

            //assert
            Assert.AreNotSame(objfoo, anotherObjfoo);
        }
    }
}
