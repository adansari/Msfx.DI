using Msfx.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Lab
{
    [Injectable]
    abstract class Animal
    {
        public abstract void MakeSound();
    }

    [Injectable]
    class Dog : Animal
    {
        public string Color { get; }
        public Dog(string color)
        {
            Color = color;
        }
        public override void MakeSound()
        {
            Console.WriteLine(Color + " Dog makes sound, Bhow Bhow !!!");
        }
    }

    [Injectable]
    class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Meow Meow !!!");
        }
    }
}

namespace Msfx.DI.Lab.Animals
{
    [Injectable]
    class Cat { }
}