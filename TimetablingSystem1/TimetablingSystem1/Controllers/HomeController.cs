using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TimetablingSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            //string Url = "~/Home";

            //RequestContext.HttpContext.Request.ApplicationPath + Url.SubString(2);
            return View();
        }
	}
}