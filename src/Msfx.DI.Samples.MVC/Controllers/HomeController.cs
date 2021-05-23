using Msfx.DI.Attributes;
using Msfx.DI.Samples.MVC.App_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Msfx.DI.Samples.MVC.Controllers
{
    public class HomeController : Controller
    {

        [AutoInject]
        public HomeService Service { get; set; }
        public ActionResult Index()
        {
            ViewBag.HomeService = Service.GetIndexData();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}