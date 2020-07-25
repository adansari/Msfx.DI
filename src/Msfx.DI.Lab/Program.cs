using Msfx.DI.Attributes;
using Msfx.DI.LifeTimeManagers;
using System;

namespace Msfx.DI.Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            DIContext dIContext = new AttributeBasedDIContext(typeof(Program));
            dIContext.Scan();

            Console.Read();
        }
    }

    [Injectable]
    public abstract class Animal
    {
        public abstract void MakeSound();
    }

    [Injectable]
    public interface IAnimal
    {
       void Eat();
    }

    [Injectable]
    [InjectForType(typeof(Animal))]
    [InjectForType(typeof(IAnimal))]

    public class Dog : Animal, IAnimal
    {
        public void Eat()
        {
            Console.WriteLine("Dog eating bone");
        }

        public override void MakeSound()
        {
            Console.WriteLine("Bhow Bhow!!!!");
        }
    }

    [Injectable]
    [CanBeInjectedFor(typeof(Animal))]
    [CanBeInjectedFor(typeof(IAnimal))]
    public class Cat : Animal, IAnimal
    {
        public void Eat()
        {
            Console.WriteLine("Cat eating fish");
        }

        public override void MakeSound()
        {
            Console.WriteLine("Meow Meow!!!!");
        }
    }

    [Injectable]
    public class Foo
    {
        public string State { get; set; }
    }

    [Injectable(InstanceType=InstanceType.Local)]
    public class FooLocal
    {
        public string State { get; set; } = "default";
    }
}
