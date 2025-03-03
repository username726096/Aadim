using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AadimPark.Controllers
{
    public class DashboardController : Controller
    {

        [HttpGet]
        public ActionResult dashboard()
        {
            return View();
        }
     
    }
}