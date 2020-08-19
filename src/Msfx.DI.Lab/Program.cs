using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Msfx.DI.Attributes;

namespace Msfx.DI.Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            var dIContext = new AttributeBasedDIContext(typeof(Program)).Scan();

            var computer = dIContext.Inject<Desktop>();

            computer.Boot();

            Console.ReadLine();
        }
    }
}
