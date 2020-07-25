using FakeAssembly;
using FakeTypes.For.NonDITests;
using FakeTypes.For.ScannerTest.Foo1;
using FakeTypes.For.ScannerTest.Foo1.Foo2;
using FakeTypes.For.ScannerTest.Foo1.Foo2.Foo3;
using FakeTypes.For.ScannerTest.Foo1.Foo2.Foo3.Foo4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Msfx.DI.Scanners.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AssemblyScannerTests
    {
        Mock<TestAssembly> mockAssembly;

        [TestInitialize]
        public void InitForEachTests()
        {
            mockAssembly = MockAssembly.GetMockWithTypeSetup(NamesapceSetupLevel.All);
        }

        [TestMethod]
        public void AssemblyScanner_Scan_Test()
        {
            //arrange
            AssemblyScanner assemblyScanner = new AssemblyScanner((Assembly)mockAssembly.Object);

            //act
            var types = assemblyScanner.Scan();

            //Assert
            Assert.IsTrue(types.Contains(typeof(foo)));
            Assert.IsTrue(types.Contains(typeof(abs_foo_1)));
            Assert.IsTrue(types.Contains(typeof(Ifoo_2)));
            Assert.IsTrue(types.Contains(typeof(foo_3)));
            Assert.IsTrue(types.Contains(typeof(foo_4)));
        }
    }
}

