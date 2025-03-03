using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AadimPark.Controllers
{
    public class DeleteController : Controller
    {
        AadimParkEntities2 db = new AadimParkEntities2();
        
        public ActionResult Index()
        {

            var a = new DeleteVM
            {

                Deletes = db.deleted_entry.ToList(),
            };
            return View(a);
        
        }
    }
}