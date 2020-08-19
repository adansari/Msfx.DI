using Msfx.DI.Attributes;
using System;

namespace Msfx.DI.Lab
{
    [Injectable]
    public abstract class Display { public abstract void TurnON(); }

    [Injectable]
    public class CRTMonitor : Display
    {
        public override void TurnON() { Console.WriteLine("CRTMonitor starting"); }
    }

    [Injectable]
    [InjectFor(typeof(Display))]
    public class LCDMonitor : Display
    {
        public override void TurnON() { Console.WriteLine("LCD starting"); }
    }
}
