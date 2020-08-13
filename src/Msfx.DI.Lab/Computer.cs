using Msfx.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Lab
{
    [Injectable]
    public abstract class Processor
    {
        public abstract void Compute();
    }

    [Injectable]
    [InjectForType(typeof(Processor))]
    public class Intel : Processor
    {
        public override void Compute()
        {
            Console.WriteLine("Intel computing...");
        }
    }


    [Injectable]
    public class AMD : Processor
    {
        public override void Compute()
        {
            Console.WriteLine("AMD computing...");
        }
    }

    [Injectable]
    public abstract class Display
    {
        public virtual Panel Panel { get; set; }
        public abstract void Print();
    }

    [Injectable]
    public class CRTMonitor : Display
    {
        [AutoInject]
        [PreferredType(typeof(CRTMonitorPanel))]
        public override Panel Panel { get; set; }
        public override void Print()
        {
            Console.WriteLine("CRTMonitor printing");
        }
    }


    [Injectable]
    [InjectForType(typeof(Display))]
    public class LCDMonitor : Display
    {
        [AutoInject]
        public override Panel Panel { get; set; }
        public override void Print()
        {
            Console.WriteLine("LCD printing");
        }
    }

    [Injectable]
    public abstract class Panel
    {
        public abstract void Contorl();
    }

    [Injectable]
    public class CRTMonitorPanel : Panel
    {
        public override void Contorl()
        {
            Console.WriteLine("CRTMonitorPanel control");
        }
    }

    [Injectable]
    [InjectForType(typeof(Panel))]
    public class LCDMonitorPanel : Panel
    {
        public override void Contorl()
        {
            Console.WriteLine("LCDMonitorPanel control");
        }
    }

    [Injectable]
    public abstract class Computer
    {
        public virtual Display Display { get; set; }
        public virtual Processor Processor {get;set;}
        public abstract void Operate();
    }

    [Injectable]
    public class Desktop : Computer
    {
        [AutoInject]
        [PreferredType(typeof(CRTMonitor))]
        public override Display Display { get; set; }

        [AutoInject]
        [PreferredType(typeof(AMD))]
        public override Processor Processor { get; set; }
        public override void Operate()
        {
            Console.WriteLine("Desktop operataing");

            Processor.Compute();

            Display.Print();

            Display.Panel.Contorl();
        }
    }

    [Injectable]
    public class Laptop : Computer
    {
        public Laptop() { }

        [AutoInject]
        public Laptop([PreferredType(typeof(CRTMonitor))]Display display)
        {
            this.Display = display;
        }


        [AutoInject]
        public void SetProcessor([PreferredType(typeof(AMD))]Processor processor)
        {
            this.Processor = processor;
        }

        public override void Operate()
        {
            Console.WriteLine("Laptop operataing");

            Processor.Compute();

            Display.Print();

            Display.Panel.Contorl();
        }
    }
}
