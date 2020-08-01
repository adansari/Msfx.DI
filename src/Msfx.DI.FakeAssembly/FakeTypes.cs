using Msfx.DI;
using Msfx.DI.Attributes;
using Msfx.DI.LifetimeManagers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakeTypes.For.NonDITests
{
    [ExcludeFromCodeCoverage]
    public class foo { }

    public interface Ifoo { }

    [ExcludeFromCodeCoverage]
    public abstract class abs_foo { }
}

namespace FakeTypes.For.DITests
{
    [ExcludeFromCodeCoverage]
    public class Boot {}

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class foo
    {
        public foo() { }
        public foo(string prop) { strProp = prop; }
        public string strProp { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class _foo { }

    [ExcludeFromCodeCoverage]
    [Injectable(InstanceType = InstanceType.Local)]
    public class fooLocal
    {
        public string strProp { get; set; }
    }

    [Injectable]
    public interface Ifoo { }
    public interface _Ifoo { }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public abstract class abs_foo { }

    [ExcludeFromCodeCoverage]
    public abstract class _abs_foo { }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public abstract class Animal
    {
        public abstract void Sound();
    }

    [Injectable]
    public interface IMammal
    {
        void Feed();
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    [InjectForType(typeof(IMammal))]
    [CanBeInjectedFor(typeof(IMammal))]
    [CanBeInjectedFor(typeof(Animal))]
    public class Cat : Animal, IMammal
    {
        public void Feed() { }

        public override void Sound() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    [InjectForType(typeof(Animal))]
    [CanBeInjectedFor(typeof(IMammal))]
    public class Dog : Animal, IMammal
    {
        public void Feed() { }

        public override void Sound() { }
    }
}

namespace FakeTypes.For.DITests.NamespaceRecur
{
    [ExcludeFromCodeCoverage]
    [Injectable]
    public abstract class Animal
    {
        public abstract void Sound();
    }

    [Injectable]
    public interface IMammal
    {
        void Feed();
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    [InjectForType(typeof(IMammal))]
    [CanBeInjectedFor(typeof(IMammal))]
    [CanBeInjectedFor(typeof(Animal))]
    public class Cat : Animal, IMammal
    {
        public void Feed() { }

        public override void Sound() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    [InjectForType(typeof(Animal))]
    [CanBeInjectedFor(typeof(IMammal))]
    public class Dog : Animal, IMammal
    {
        public void Feed() { }

        public override void Sound() { }
    }
}

namespace FakeTypes.For.ActivatorInstanceFactoryTests
{
    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Public_Ctor
    {
        public Public_Ctor() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Public_Ctor_With_Params
    {
        public Public_Ctor_With_Params(string foo, int intfoo) { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Protected_Crot
    {
        protected Protected_Crot() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Protected_Crot_With_Params
    {
        protected Protected_Crot_With_Params(string foo, int intfoo) { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Private_Ctor
    {
        private Private_Ctor() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Private_Ctor_With_Params
    {
        protected Private_Ctor_With_Params(string foo, int intfoo) { }
    }
}

namespace FakeTypes.For.InjectableDependencyRegistrarTests
{
    [ExcludeFromCodeCoverage]
    [Injectable]
    public abstract class Animal
    {
        public abstract void Sound();
    }

    [Injectable]
    public interface IAnimal
    {
        void Sound();
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Dog
    {
        public void Sound()
        {
            string sound = "Meow Meow";
        }
    }
}

namespace FakeTypes.For.CanBeInjectedDependencyRegistrarTests
{

    [ExcludeFromCodeCoverage]
    public abstract class Animal
    {
        public abstract void Sound();
    }

    [ExcludeFromCodeCoverage]
    [CanBeInjectedFor(typeof(Monkey))]
    public class Monkey : Animal
    {
        public override void Sound()
        {
            string sound = "Monkey sound";
        }
    }

    [ExcludeFromCodeCoverage]
    [CanBeInjectedFor(typeof(Animal))]
    public class Donkey : Animal
    {
        public override void Sound()
        {
            string sound = "Donkey sound";
        }
    }

    [ExcludeFromCodeCoverage]
    [CanBeInjectedFor(typeof(Animal))]
    public class Dog : Animal
    {
        public override void Sound()
        {
            string sound = "Dog sound";
        }
    }
}

namespace FakeTypes.For.InjectForTypeDependencyRegistrarTests
{
    [ExcludeFromCodeCoverage]
    public abstract class Animal
    {
        public abstract void Sound();
    }

    [ExcludeFromCodeCoverage]
    [InjectForType(typeof(Animal))]
    public class Donkey : Animal
    {
        public override void Sound()
        {
            string sound = "Donkey sound";
        }
    }

    [ExcludeFromCodeCoverage]
    [InjectForType(typeof(Monkey))]
    public class Monkey : Animal
    {
        public override void Sound()
        {
            string sound = "Monkey sound";
        }
    }

    [ExcludeFromCodeCoverage]
    [InjectForType(typeof(Animal))]
    public class Dog : Animal
    {
        public override void Sound()
        {
            string sound = "Dog sound";
        }
    }

}

namespace FakeTypes.For.ScannerTest.Foo1
{
    [ExcludeFromCodeCoverage]
    public class foo_1 { }

    public interface Ifoo_1 { }

    [ExcludeFromCodeCoverage]
    public abstract class abs_foo_1 { }
}

namespace FakeTypes.For.ScannerTest.Foo1.Foo2
{
    [ExcludeFromCodeCoverage]
    public class foo_2 { }

    public interface Ifoo_2 { }

    [ExcludeFromCodeCoverage]
    public abstract class abs_foo_2 { }
}

namespace FakeTypes.For.ScannerTest.Foo1.Foo2.Foo3
{
    [ExcludeFromCodeCoverage]
    public class foo_3 { }

    public interface Ifoo_3 { }

    [ExcludeFromCodeCoverage]
    public abstract class abs_foo_3 { }
}

namespace FakeTypes.For.ScannerTest.Foo1.Foo2.Foo3.Foo4
{
    [ExcludeFromCodeCoverage]
    public class foo_4 { }

    public interface Ifoo_4 { }

    [ExcludeFromCodeCoverage]
    public abstract class abs_foo_4 { }
}


