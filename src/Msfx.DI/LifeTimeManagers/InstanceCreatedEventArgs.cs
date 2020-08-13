using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.LifetimeManagers
{
    public class InstanceCreatedEventArgs : EventArgs
    {
        public object Instance { get; set; }

        public Type InstanceType { get; set; }
    }
}
