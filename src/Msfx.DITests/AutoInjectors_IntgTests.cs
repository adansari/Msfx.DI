using System;
using FakeTypes.For.AutoInjectors;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Msfx.DI.Tests
{
    [ExcludeFromCodeCoverage]
	[TestClass]
	public class AutoInjectors_IntgTests
	{
        DIContext diCtx;

        [TestInitialize]
        public void Init()
        {
            diCtx = new AttributeBasedDIContext(typeof(foo)).Scan();
        }

        [TestMethod]
		public void AutoInjectors_Ctor_Injection_IntgTests()
		{
            //arrage
            Computer pc;

            //act
            pc = diCtx.Inject<Desktop>();

            //assert
            Assert.IsInstanceOfType(pc.RAM, typeof(DDRRAM));
            Assert.IsNotNull(pc.RAM);
            Assert.AreEqual(8, pc.RAM.Size);

        }

        [TestMethod]
        public void AutoInjectors_Method_Injection_IntgTests()
        {
            //arrage
            Computer pc;

            //act
            pc = diCtx.Inject<Desktop>();

            //assert
            Assert.IsInstanceOfType(pc.Processor, typeof(AMD));
        }

        [TestMethod]
        public void AutoInjectors_Prop_Injection_IntgTests()
        {
            //arrage
            Laptop pc;

            //act
            pc = diCtx.Inject<Laptop>();

            //assert
            Assert.IsInstanceOfType(pc.RAM, typeof(DDRRAM));
            Assert.IsNotNull(pc.RAM);
            Assert.AreEqual(16, pc.RAM.Size);
        }

        [TestMethod]
        public void AutoInjectors_Field_Injection_IntgTests()
        {
            //arrage
            Laptop pc;

            //act
            pc = diCtx.Inject<Laptop>();

            //assert
            Assert.IsInstanceOfType(pc.AnotherRAM, typeof(RAM));
            Assert.IsNotNull(pc.AnotherRAM);
            Assert.AreEqual(8, pc.AnotherRAM.Size);

        }

        [TestMethod]
        public void AutoInjectors_Multilevel_Injection_IntgTests()
        {
            //arrage
            Three three;

            //act
            three = diCtx.Inject<Three>();

            //assert
            Assert.IsNotNull(three.Two.One);

        }
    }
}
