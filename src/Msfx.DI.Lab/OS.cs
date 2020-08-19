using Msfx.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Lab
{
    [Injectable]
    public abstract class OS { public string Version {get; protected set;} public abstract void Operate(); }

    [Injectable]
    [InjectFor(typeof(OS))]
    public class Windows : OS
    {
        private Windows() { }
        public Windows(string version) { Version = version; }
        public override void Operate() { Console.WriteLine("Windows operating..."); }
    }

    [Injectable]
    public class Linux : OS
    {
        private Linux() { }
        public Linux(string version) { Version = version; }
        public override void Operate() { Console.WriteLine("Linux operating..."); }
    }
}
