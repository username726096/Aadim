using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AadimPark.Controllers
{
    public class AreaController : Controller
    {
      AadimParkEntities2 db = new AadimParkEntities2();

        [HttpGet]
        public ActionResult AreaWatch()
        {

            var area = db.Areas
.Select(c => new SelectListItem
{
  Value = c.id.ToString(),
  Text = c.Name
})
.ToList();
            ViewBag.Areas = area;

            return View();


        }

        public ActionResult Index()
        {
            var data = new AreaVM
            {
                Areas = db.Areas.ToList(),
                Area_Details = db.Area_details.ToList() 

            };
            return View(data);
           
        }

        [HttpGet]
        public ActionResult Add()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Add(AreaVM info)
        {
            var master = new Area
            {
                Name = info.Name,
                Address = info.Address,
                Total_capacity = info.Total_capacity,

            };
            db.Areas.Add(master);
            db.SaveChanges();

            var details = new Area_details
            {
                fourWheel_capacity = info.fourWheel_capacity,
                twoWheel_capacity = info.twoWheel_capacity,
                fourWheel_occupied = 0,
                twoWheel_occupied = 0,
                Area_id = master.id,
                two_price = info.two_price,
                four_price = info.four_price,



            };
            db.Area_details.Add(details);
            db.SaveChanges();
            return Json(new { Sucess = true});
        }
        [HttpGet]

        public ActionResult Edit(int id)
        {

            var Area = db.Areas.FirstOrDefault(a => a.id == id);
            if (Area == null)
            {
                return HttpNotFound();

            }
            var aread = db.Area_details.Where(x => x.Area_id == id).ToList();
            var viewModel = new AreaVM
            {
                Area = Area,
                AreaDetails = aread,
            };

            return View(viewModel);


        }
        [HttpPost]
        public ActionResult Edit(AreaVM info)
        {
            var area = db.Areas.SingleOrDefault(u => u.id == info.id);

            if (area == null)
            {
                return HttpNotFound();
            }
            area.Name = info.Name;
            area.Address = info.Address;
            area.Total_capacity = info.Total_capacity;
            var areaDetails = db.Area_details.SingleOrDefault(u => u.Area_id == info.id);

            areaDetails.four_price = info.four_price;
            areaDetails.two_price = info.two_price;
            areaDetails.fourWheel_capacity = info.fourWheel_capacity;
            areaDetails.twoWheel_capacity = info.twoWheel_capacity;









            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
      
            var user = db.Area_details.Where(u => u.Area_id == id).FirstOrDefault(); 
            if (user != null) { 
            db.Area_details.Remove(user);
            db.SaveChanges();
            }

            var user2 = db.Areas.SingleOrDefault(u => u.id == id);
            if (user2 != null) { 
            db.Areas.Remove(user2);
            db.SaveChanges();
            }
            var a = new deleted_entry
            {



                descrpition = "Wrong Area"
            };
            db.deleted_entry.Add(a);
            db.SaveChanges();

            return Json(new { sucess = true });


           
        }






















        [HttpPost]
        public ActionResult AreaWatch(Area_details data)
        {
            var area = db.Areas
.Select(c => new SelectListItem 
{
  Value = c.id.ToString(),
  Text = c.Name
})
.ToList();
            ViewBag.Areas=area;
            int areaId = Convert.ToInt32(data.Area_id);
            if (data.Area_id != null)
            {
                var a = db.Area_details.Where(x => x.Area_id == (areaId)).FirstOrDefault();



                if (a == null)
                {
                    return Json(new { error = "Area not found" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var returnData = new Area_details
                    {
                        fourWheel_capacity = a.fourWheel_capacity,
                        fourWheel_occupied = a.fourWheel_occupied,
                        twoWheel_occupied = a.twoWheel_occupied,
                        twoWheel_capacity = a.twoWheel_capacity,


                    };
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new {Sucess=false}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}