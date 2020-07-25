using FakeAssembly;
using FakeTypes.For.ScannerTest.Foo1;
using FakeTypes.For.ScannerTest.Foo1.Foo2;
using FakeTypes.For.ScannerTest.Foo1.Foo2.Foo3;
using FakeTypes.For.ScannerTest.Foo1.Foo2.Foo3.Foo4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Msfx.DI.Scanners.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class NamespaceRecursiveScannerTests
    {
        [TestMethod]
        public void NamespaceRecursiveScanner_Scan_Base_NS_Foo1_Test()
        {
            //arrange
            IEnumerable<Type> types;
            Mock<TestAssembly> mockAssembly = MockAssembly.GetMockWithTypeSetup(NamesapceSetupLevel.All);
            NamespaceRecursiveScanner nsrecursiveScanner = new NamespaceRecursiveScanner((Assembly)mockAssembly.Object, "FakeTypes.For.ScannerTest.Foo1");

            //act
            types = nsrecursiveScanner.Scan();

            //assert
            Assert.IsTrue(types.Contains(typeof(foo_1)));
            Assert.IsTrue(types.Contains(typeof(Ifoo_1)));
            Assert.IsTrue(types.Contains(typeof(abs_foo_1)));
            Assert.IsTrue(types.Contains(typeof(foo_2)));
            Assert.IsTrue(types.Contains(typeof(Ifoo_2)));
            Assert.IsTrue(types.Contains(typeof(abs_foo_2)));
        }

        [TestMethod]
        public void NamespaceRecursiveScanner_Scan_Base_NS_Foo2_Test()
        {
            //arrange
            IEnumerable<Type> types;
            Mock<TestAssembly> mockAssembly = MockAssembly.GetMockWithTypeSetup(NamesapceSetupLevel.All);
            NamespaceRecursiveScanner nsrecursiveScanner = new NamespaceRecursiveScanner((Assembly)mockAssembly.Object, "FakeTypes.For.ScannerTest.Foo1.Foo2");

            //act
            types = nsrecursiveScanner.Scan();

            //assert
            Assert.IsFalse(types.Contains(typeof(foo_1)));
            Assert.IsTrue(types.Contains(typeof(foo_2)));
            Assert.IsTrue(types.Contains(typeof(foo_3)));
            Assert.IsTrue(types.Contains(typeof(foo_4)));

        }
    }
}
