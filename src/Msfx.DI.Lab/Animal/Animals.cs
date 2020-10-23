using Msfx.DI.Attributes;
using System;

namespace Msfx.DI.Lab
{
    [Injectable]
    public abstract class Animal
    {
        public abstract void MakeSound();
    }

    [Injectable]
    public class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Meowwww");
        }
    }
}
