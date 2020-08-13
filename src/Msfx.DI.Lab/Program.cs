using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Msfx.DI.Attributes;
using Msfx.DI.Lab.Levels;

namespace Msfx.DI.Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            DIContext di = new AttributeBasedDIContext(typeof(Program)).Scan();

            Computer desktop = di.Inject<Desktop>();

            desktop.Operate();


            Computer Laptop = di.Inject<Laptop>();

            Laptop.Operate();

            Console.Read();
        }
    }
}
