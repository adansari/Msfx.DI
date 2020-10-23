using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Msfx.DI.Attributes;
using Msfx.DI.Lab.RealWorld;
using Msfx.DI.Scanners;

namespace Msfx.DI.Lab
{
    class Program
    {

        static void Main(string[] args)
        {
            var dIContext = new AttributeBasedDIContext(typeof(Program),DependencyScanTarget.CurrentNamespaceRecursive).Scan();

            IPoke  poker = dIContext.InjectByName<IPoke>("WebPoke","https://www.google.com");

            Console.WriteLine(poker.Poke());

            Console.ReadLine();
        }
    }
}
