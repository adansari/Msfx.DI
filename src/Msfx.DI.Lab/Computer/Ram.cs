using Msfx.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Lab
{
    [Injectable]
    public class RAM
    {
        protected RAM() { }

        protected int _sizeInGB;
        public RAM(int sizeInGB) { this._sizeInGB = sizeInGB; }

        public virtual void GetReady() { Console.WriteLine(_sizeInGB + " GB - RAM getting ready"); }
    }

    [Injectable]
    public class DDRRAM : RAM
    {
        protected DDRRAM() { }
        public DDRRAM(int sizeInGB) : base(sizeInGB) { }

        public override void GetReady() { Console.WriteLine(_sizeInGB + " GB - DDR RAM getting ready"); }
    }
}
