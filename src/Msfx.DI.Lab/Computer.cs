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
    [InjectFor(typeof(Processor))]
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
        [Inject(typeof(CRTMonitorPanel))]
        public override Panel Panel { get; set; }
        public override void Print()
        {
            Console.WriteLine("CRTMonitor printing");
        }
    }


    [Injectable]
    [InjectFor(typeof(Display))]
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
    [InjectFor(typeof(Panel))]
    public class LCDMonitorPanel : Panel
    {
        public override void Contorl()
        {
            Console.WriteLine("LCDMonitorPanel control");
        }
    }

    [Injectable]
    public class RAM
    {
        protected RAM() { }

        protected int _sizeInGB;
        public RAM(int sizeInGB) { this._sizeInGB = sizeInGB; }

        public virtual void GetSize()
        {
            Console.WriteLine(_sizeInGB + " GB");
        }
    }

    [Injectable]
    public class DDRRAM : RAM
    {
        protected DDRRAM() { }
        public DDRRAM(int sizeInGB) : base(sizeInGB) { }

        public override void GetSize()
        {
            Console.WriteLine(_sizeInGB + " GB - DDR");
        }
    }

    [Injectable]
    public abstract class Computer
    {
        public virtual Display Display { get; set; }
        public virtual Processor Processor {get;set;}
        public virtual RAM RAM { get; set; }

        public abstract void Operate();
    }

    [Injectable]
    public class Desktop : Computer
    {
        private Display _display;

        [AutoInject]
        [Inject(typeof(CRTMonitor))]
        public override Display Display
        {
            get
            {
                return this._display;
            }
            set
            {
                this._display = value;
            }
        }

        [AutoInject]
        [Inject(typeof(AMD))]
        public override Processor Processor { get; set; }

        [AutoInject]
        [InjectValue(8)]
        public override RAM RAM { get;set; }
        public override void Operate()
        {
            Console.WriteLine("Desktop operataing");

            this.RAM.GetSize();

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
        public Laptop([Inject(typeof(CRTMonitor))]Display display)
        {
            this.Display = display;
        }

        [AutoInject]
        [InjectValue(16)]
        [Inject(typeof(DDRRAM))]
        public override RAM RAM { get; set; }

        [AutoInject]
        public void SetProcessor([Inject(typeof(AMD))]Processor processor)
        {
            this.Processor = processor;
        }

        public override void Operate()
        {
            Console.WriteLine("Laptop operataing");

            this.RAM.GetSize();

            Processor.Compute();

            Display.Print();

            Display.Panel.Contorl();
        }
    }
}
