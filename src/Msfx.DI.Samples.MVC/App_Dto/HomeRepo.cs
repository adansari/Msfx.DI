using Msfx.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msfx.DI.Samples.MVC.App_Dto
{
    [Injectable]
    public class HomeRepo
    {
        public string GetIndexData()
        {
            return "Data from Home Repo";
        }
    }
}