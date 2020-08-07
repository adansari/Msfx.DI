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
            DIContext di = new AttributeBasedDIContext(typeof(Program), Scanners.DependencyScanTarget.CurrentNamespaceRecursive).Scan();

            //Level5 level5 = di.Inject<Level5>();

            Animal dog = di.InjectByName<Animal>("Dog","Black");
            dog.MakeSound();

            Animal cat = di.InjectByName<Animal>("DI.Lab.Cat");
            cat.MakeSound();

            Animal none = di.InjectByName<Animal>("Tiger");
            none.MakeSound();

            Console.Read();
        }
    }
}
