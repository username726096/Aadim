using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AadimPark.Controllers
{
    public class AdminController : Controller
    {
        AadimParkEntities2 db = new AadimParkEntities2();


        public ActionResult Dashboard()
        {
            var area = db.Areas
.Select(d => new SelectListItem
{
    Value = d.id.ToString(),
    Text = d.Name
})
.ToList();
            ViewBag.Areas = area;
            return View();
        }
        [HttpPost]

        public ActionResult Add(Admin_list info)

        {
            var area = db.Users.Where(x => x.Role =="Admin")
.Select(c => new SelectListItem
{
   Value = c.id.ToString(),
   Text = c.Username.ToString()
})
.ToList();
            var area2 = db.Areas
.Select(c => new SelectListItem
{
    Value = c.Name.ToString(),
    Text = c.id.ToString()
})
.ToList();
            ViewBag.adminid = area;
            ViewBag.areaid = area2;
            var a = new Admin_list
            {
                Admin_id = Convert.ToInt32(info.Admin_id),
                Area_id = Convert.ToInt32(info.Area_id),

            };
            db.Admin_list.Add(a);
            db.SaveChanges();

            return View();
        }
       
        public ActionResult Index()
        {
            var data = new AdminVM
            {
                awd = db.Admin_list.ToList(),

            };
            return View(data);

           
        }

        [HttpGet]
        public ActionResult Add()
        {
            var area = db.Users.Where(x => x.Role == "Admin")
  .Select(c => new SelectListItem
  {
      Value = c.id.ToString(),
      Text = c.Username.ToString()
  })
  .ToList();
            var area2 = db.Areas
.Select(c => new SelectListItem
{
    Value = c.Name.ToString(),
    Text = c.id.ToString()
})
.ToList();
            ViewBag.adminid = area;
            ViewBag.areaid = area2;

            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var a = db.Admin_list.Where(x => x.id == id ).FirstOrDefault();
            
            db.Admin_list.Remove(a);
            db.SaveChanges();

            return Json(new { success = true });
        }
        [HttpGet]
        public ActionResult Delete()
        {

            return View();
        }


    }
}