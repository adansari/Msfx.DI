using System;
using System.Reflection;

namespace Msfx.DI.Factories
{
    public class ActivatorInstanceFactory : InstanceFactory
    {
        public override object CreateInstance(Type type, object[] args)
        {
            return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, args, null);
        }
    }
}