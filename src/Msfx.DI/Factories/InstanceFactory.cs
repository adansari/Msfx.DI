using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Factories
{
    public abstract class InstanceFactory
    {
        public abstract object CreateInstance(Type type, object[] args);

        public static InstanceFactory GetFactory()
        {
            return new ActivatorInstanceFactory();
        }
    }
}
