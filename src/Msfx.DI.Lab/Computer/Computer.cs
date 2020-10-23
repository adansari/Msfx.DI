using Msfx.DI.Attributes;
using Msfx.DI.LifetimeManagers;
using System;

namespace Msfx.DI.Lab
{
    [Injectable]
    public abstract class Computer
    {
        protected OS _os;
        protected Processor _processor;
        public virtual Display Display { get; set; }
        public virtual Processor Processor { get { return this._processor; } }
        public virtual OS OS { get { return this._os; } }
        public virtual RAM RAM { get; set; }
        public abstract void Boot();
        public abstract void InstallOS(OS osToInstall);

    }

    [Injectable(InstanceType = InstanceType.Local)]
    public class Desktop : Computer
    {
        protected Desktop() { }

        [AutoInject]
        public Desktop([Inject(typeof(AMD))] Processor processor)
        {
            this._processor = processor;
        }

        [AutoInject]
        [Inject(typeof(CRTMonitor))]
        public override Display Display { get; set; }

        [AutoInject]
        [InjectValue(8)]
        public override RAM RAM { get; set; }
        
        [AutoInject]
        public override void InstallOS([InjectValue("20")][Inject(typeof(Linux))] OS osToInstall)
        {
            this._os = osToInstall;
        }

        public override void Boot()
        {
            Console.WriteLine("System booting...");
            Display.TurnON();
            RAM.GetReady();
            Processor.Compute();
            OS.Operate();
        }
    }
}
