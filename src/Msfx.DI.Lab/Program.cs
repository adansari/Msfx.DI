using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Msfx.DI.Attributes;

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

    class Program
    {
        static void Main(string[] args)
        {
            DIContext di = new AttributeBasedDIContext(typeof(Program)).Scan();

            Animal cat = di.Inject<Cat>();
            cat.MakeSound();

            Console.ReadLine();
        }
    }
}
