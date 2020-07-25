using FakeTypes.For.NonDITests;
using FakeTypes.For.ScannerTest.Foo1;
using FakeTypes.For.ScannerTest.Foo1.Foo2;
using FakeTypes.For.ScannerTest.Foo1.Foo2.Foo3;
using FakeTypes.For.ScannerTest.Foo1.Foo2.Foo3.Foo4;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace FakeAssembly
{
    [ExcludeFromCodeCoverage]
    public class TestAssembly : Assembly { }

    [ExcludeFromCodeCoverage]
    public class MockAssembly
    {
        public static Type[] NonInjectableTypes = new Type[] {
                typeof(foo),
                typeof(Ifoo),
                typeof(abs_foo) };

        public static Type[] Foo_1Types = new Type[] {
                typeof(foo_1),
                typeof(Ifoo_1),
                typeof(abs_foo_1) };

        public static Type[] Foo_2Types = new Type[] {
                typeof(foo_2),
                typeof(Ifoo_2),
                typeof(abs_foo_2) };

        public static Type[] Foo_3Types = new Type[] {
                typeof(foo_3),
                typeof(Ifoo_3),
                typeof(abs_foo_3) };

        public static Type[] Foo_4Types = new Type[] {
                typeof(foo_4),
                typeof(Ifoo_4),
                typeof(abs_foo_4) };

        public static Type[] InjectableTypes = new Type[] {
                typeof(FakeTypes.For.DITests.foo),
                typeof(FakeTypes.For.DITests._foo),
                typeof(FakeTypes.For.DITests.Ifoo),
                typeof(FakeTypes.For.DITests._Ifoo),
                typeof(FakeTypes.For.DITests.abs_foo),
                typeof(FakeTypes.For.DITests._abs_foo),
                typeof(FakeTypes.For.DITests.Animal),
                typeof(FakeTypes.For.DITests.Cat),
                typeof(FakeTypes.For.DITests.Dog)
        };

        public static Mock<TestAssembly> GetMock()
        {
            Mock<TestAssembly> mockAssembly = new Mock<TestAssembly>();
            mockAssembly.SetupGet(a => a.FullName).Returns(Assembly.GetExecutingAssembly().FullName);
            return mockAssembly;
        }

        public static Mock<TestAssembly> GetMockWithDefaultTypeSetup()
        {
            Mock<TestAssembly>  mockAssembly = GetMock();
            mockAssembly.Setup(a => a.GetTypes()).Returns(NonInjectableTypes);
            mockAssembly.SetupGet(a => a.FullName).Returns(Assembly.GetExecutingAssembly().FullName);
            return mockAssembly;
        }

        public static Mock<TestAssembly> GetMockWithTypeSetup(NamesapceSetupLevel level)
        {
            Mock<TestAssembly> mockAssembly = GetMock();
            mockAssembly.SetupGet(a => a.FullName).Returns(Assembly.GetExecutingAssembly().FullName);
            switch (level)
            {
                case NamesapceSetupLevel.NonInjectableTypes:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(NonInjectableTypes);
                    break;
                case NamesapceSetupLevel.NonInjectableTypes_and_Foo_1Types:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(NonInjectableTypes.Concat(Foo_1Types).ToArray());
                    break;
                case NamesapceSetupLevel.Foo_1Types:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(Foo_1Types);
                    break;
                case NamesapceSetupLevel.Foo_1Types_and_Foo_2Types:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(Foo_1Types.Concat(Foo_2Types).ToArray());
                    break;
                case NamesapceSetupLevel.Foo_2Types:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(Foo_2Types);
                    break;
                case NamesapceSetupLevel.Foo_3Types:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(Foo_3Types);
                    break;
                case NamesapceSetupLevel.Foo_4Types:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(Foo_4Types);
                    break;
                case NamesapceSetupLevel.All:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(NonInjectableTypes.Concat(Foo_1Types).Concat(Foo_2Types).Concat(Foo_3Types).Concat(Foo_4Types).ToArray());
                    break;
                case NamesapceSetupLevel.DI:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(InjectableTypes);
                    break;
                default:
                    mockAssembly.Setup(a => a.GetTypes()).Returns(NonInjectableTypes);
                    break;
            }


            
            return mockAssembly;
        }
    }

    public enum NamesapceSetupLevel
    {
        NonInjectableTypes,
        NonInjectableTypes_and_Foo_1Types,
        Foo_1Types,
        Foo_1Types_and_Foo_2Types,
        Foo_2Types,
        Foo_3Types,
        Foo_4Types,
        All,
        DI
    }
}
