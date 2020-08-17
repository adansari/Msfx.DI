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
    [InjectFor(typeof(IMammal))]
    [CanBeInjectedFor(typeof(IMammal))]
    [CanBeInjectedFor(typeof(Animal))]
    public class Cat : Animal, IMammal
    {
        public void Feed() { }

        public override void Sound() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    [InjectFor(typeof(Animal))]
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
    [InjectFor(typeof(IMammal))]
    [CanBeInjectedFor(typeof(IMammal))]
    [CanBeInjectedFor(typeof(Animal))]
    public class Cat : Animal, IMammal
    {
        public void Feed() { }

        public override void Sound() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    [InjectFor(typeof(Animal))]
    [CanBeInjectedFor(typeof(IMammal))]
    public class Dog : Animal, IMammal
    {
        public void Feed() { }

        public override void Sound() { }
    }
}

namespace FakeTypes.For.AutoInjectors
{
    [ExcludeFromCodeCoverage]
    public class foo
    {
        private bar _bar,_pbar;

        [AutoInject]
        public bar FieldBar;

        [AutoInject]
        [Inject(typeof(bar))]
        public bar PropBar { get; set; }

        public foo PropFoo{ get; set; }

        [InjectValue(20)]
        public pow PropPow { get; set; }

        public foo() { }

        [AutoInject]
        public foo(bar bar)
        {
            this._bar = bar;
        }
        
        public void DoFoo(foo pfoo, [Inject(typeof(bar))]bar pbar) { }

        public void DoPow([InjectValue(10)]pow bow) { }


        [AutoInject]
        public void DoBar(bar pbar) { this._pbar = pbar; }

        public bar Get_bar() { return this._bar; }

        public bar Get_pbar() { return this._pbar; }
    }

    [ExcludeFromCodeCoverage]
    public class bar
    {
    }

    [ExcludeFromCodeCoverage]
    public class pow
    {
        public pow(int intVal){}
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public abstract class Processor
    {
        public abstract void Compute();
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Intel: Processor
    {
        public override void Compute() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class AMD : Processor
    {
        public override void Compute() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class RAM
    {
        protected RAM() { }

        protected int _sizeInGB;
        public RAM(int sizeInGB) { this._sizeInGB = sizeInGB; }

        public int Size { get { return this._sizeInGB; } }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class DDRRAM:RAM
    {
        protected DDRRAM() { }
        public DDRRAM(int sizeInGB) : base(sizeInGB) { }

    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public abstract class Computer
    {
        protected Processor _processor;

        protected RAM _ram;
        public abstract void Operate();

        public Processor Processor { get { return this._processor; } }
        public RAM RAM { get { return this._ram; } }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Desktop:Computer
    {
        // A private or protected ctor is a must for dependency injection
        protected Desktop(){}

        [AutoInject]
        public Desktop(
            [Inject(typeof(DDRRAM))]
            [InjectValue(8)]
            RAM ram)
        {
            this._ram = ram;
        }

        [AutoInject]
        public void SetProcessor([Inject(typeof(AMD))]Processor processor)
        {
            this._processor = processor;
        }
        public override void Operate(){}
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Laptop : Computer
    {
        // A private or protected ctor is a must for dependency injection
        protected Laptop() { }

        [AutoInject]
        [InjectValue(8)]
        public RAM AnotherRAM;
         
        [AutoInject]
        [Inject(typeof(DDRRAM))]
        [InjectValue(16)]
        public RAM SetRAM { set { this._ram = value; } }

        public override void Operate() { }
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class One { }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Two
    {
        [AutoInject]
        public One One;
    }

    [ExcludeFromCodeCoverage]
    [Injectable]
    public class Three
    {
        [AutoInject]
        public Two Two;
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
    [InjectFor(typeof(Animal))]
    public class Donkey : Animal
    {
        public override void Sound()
        {
            string sound = "Donkey sound";
        }
    }

    [ExcludeFromCodeCoverage]
    [InjectFor(typeof(Monkey))]
    public class Monkey : Animal
    {
        public override void Sound()
        {
            string sound = "Monkey sound";
        }
    }

    [ExcludeFromCodeCoverage]
    [InjectFor(typeof(Animal))]
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


