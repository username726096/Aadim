using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AadimPark.Controllers
{
    public class billController : Controller
    {
        AadimParkEntities2 db = new AadimParkEntities2();

        // GET: bill
        public ActionResult Index()
        {
            var data = new billVM
            {
                bills = db.bills.ToList(),

            }; return View(data);
        }
    }
}