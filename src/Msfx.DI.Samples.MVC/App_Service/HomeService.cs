using Msfx.DI.Attributes;
using Msfx.DI.Samples.MVC.App_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msfx.DI.Samples.MVC.App_Service
{
    [Injectable]
    public class HomeService
    {
        [AutoInject]
        public HomeRepo Repo;
        public string GetIndexData()
        {
            return "Data from Home Service" + " => "+Repo.GetIndexData();
        }
    }
}