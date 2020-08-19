using Msfx.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Lab
{
    [Injectable]
    public abstract class Processor { public abstract void Compute(); }

    [Injectable]
    [InjectFor(typeof(Processor))]
    public class Intel : Processor
    {
        public override void Compute() { Console.WriteLine("Intel Processor"); }
    }

    [Injectable]
    public class AMD : Processor
    {
        public override void Compute() { Console.WriteLine("AMD Processor"); }
    }
}
