﻿using System.Web;
using System.Web.Mvc;

namespace Msfx.DI.Samples.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
