using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI
{
    public static class Extentions
    {
        public static string GetDependencyId(this Type type)
        {
            return type.FullName;
        }
    }
}
