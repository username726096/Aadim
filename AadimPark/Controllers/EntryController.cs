using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace AadimPark.Controllers
{
    public class EntryController : Controller
    {
      AadimParkEntities2 db =new AadimParkEntities2();  

        [HttpGet]
        public ActionResult Index()


        {
            var a =Convert.ToInt32(Session["Area"]);
            var data = new EntryVM
            {
                entries = db.entries.Where(x => x.Area_id == a).ToList(),

            };
            return View(data);

            


            
        }
        [HttpGet]
        public ActionResult Index2()


        {
          
            var data = new EntryVM
            {
                entries = db.entries.ToList(),

            };
            return View(data);





        }
        [HttpPost]
        public ActionResult Delete(int id)


        {

            var user = db.entries.SingleOrDefault(u => u.id == id);
            db.entries.Remove(user);
            db.SaveChanges();

            var a = new deleted_entry
            {



                descrpition = "wrong Entry"
            };
            db.deleted_entry.Add(a);
            db.SaveChanges();

            return Json(new { success = true });





        }

        [HttpPost]
        public ActionResult Entry_vechical(EntryVM info)
        {
            var Area = Convert.ToInt32(Session["Area"]);
            var data = new entry
            {
                entry_time = DateTime.Now.TimeOfDay,
                exit_time = null,
                Vechical_type = info.V_type,
                Vechical_number = info.V_number,
                booking_id = info.booking_id == 0 ? (int?)null : info.booking_id,
                Status = "IN",
                Area_id = Area,




            };
         

          
                var A = db.Area_details.Where(x => x.Area_id == Area).FirstOrDefault();

                if(data.Vechical_type == "2")
                {
                    A.twoWheel_occupied++;

                }
                if (data.Vechical_type == "4")
                {
                    A.fourWheel_occupied++;

                }

          

            db.entries.Add(data);

     
             db.SaveChanges();

          
            
                return Json(new { success = true  });
            
         
            
        }
        [HttpGet]
        public ActionResult Entry_vechical()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Exit(int id)
        {
                var A = db.entries.SingleOrDefault(x => x.id == id);
            
            A.Status = "OUT";
            var Area = Convert.ToInt32(Session["Area"]);
            var area_details = db.Area_details.Where(x => x.Area_id == Area).FirstOrDefault();

            if (A.Vechical_type == "2")
            {
                area_details.twoWheel_occupied--;
            }
            else
            {

                area_details.fourWheel_occupied--;

            }
            A.exit_time = DateTime.Now.TimeOfDay;
            
            db.SaveChanges();



            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult Exit()
        {
            var data = new EntryVM
            {
                entries = db.entries.Where(u => u.Status == "IN").ToList(),

            };


//            var cars = db.entries
//                .Where(d => d.Status == "IN")
//.Select(d => new SelectListItem
//{
//   Value = d.id.ToString(),
//   Text = d.Vechical_number.ToString(),
//})
//.ToList();
//            ViewBag.vechical = cars;

            return View(data);

       
        }
        [HttpGet]
        public ActionResult Exit2()
        {
            return Json(new { Nsuccess = true });

        }



        [HttpGet]
        public ActionResult Validate()
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
        public ActionResult Validate( AdminVM  info)
        {
          



            var a = db.Admin_list.FirstOrDefault(u => u.Area_id == info.A_Id && u.Admin_id == info.Id);
            
        
            if (a != null)
            {
                Session["Allow"] = true;
                Session["Area"] = a.Area_id;
                return Json( new { success = true });

            }
            else
            {               
                    Session["Allow"] = false;
                return Json(new { success = false });




            }
        
        }
        public ActionResult Cash(int id)
        {
           
            var a = Convert.ToInt32(Session["Area"]);

            var data = db.entries.Where(x => x.id == id).FirstOrDefault();
            var data2 = data.booking_id;
            if (data2 != null)
            {
                var data3 = db.bookings.Where(x => x.id == data2).FirstOrDefault();

                if (data3.paid == "YES")
                {
                    TempData["payStatus2"] = "already paid";
                    return Json(new { success = false, message = "Already paid" }, JsonRequestBehavior.AllowGet);

                }
            }
            var pstatus = db.bills.Where(x => x.entry_id == id).ToList();
            if (pstatus.Count > 0)
            {
                TempData["payStatus2"] = "already paid";
                return Json(new { success = false, message = "Already paid" }, JsonRequestBehavior.AllowGet);
            }


            var area = db.Areas.Where(x => x.id == a).FirstOrDefault();

            var areaDetail = db.Area_details.Where(x => x.Area_id == area.id).FirstOrDefault();

            int? unitCost = 0;
            if (data.Vechical_type == "2")
            {
                 unitCost = areaDetail.two_price;
            }
            else if (data.Vechical_type == "4")
            {
                 unitCost = areaDetail.four_price;

            }



            var amountT =  DateTime.Now.TimeOfDay - data.entry_time ;
            var hours = (int)Math.Ceiling(amountT.Value.TotalHours);
            var amount = hours * unitCost;


            var model = new bill
            {
                type = "cash",
                money = amount,
                entry_id = id,
                booking_id = data.booking_id,
            };
            db.bills.Add(model);
            db.SaveChanges();


            return Json(new { success = true, Amount = amount });
            

        }
        public ActionResult Online( int  id)
        {
            var a = Convert.ToInt32(Session["Area"]);

            var data = db.entries.Where(x => x.id == id).FirstOrDefault();
            var data2 = data.booking_id;
            if (data2 != null)
            {
                var data3 = db.bookings.Where(x => x.id == data2).FirstOrDefault();

                if (data3.paid == "YES")
                {
                    TempData["payStatus2"] = "already paid";
                    return Json(new { success = false, message = "Already paid" }, JsonRequestBehavior.AllowGet);

                }
            }
            var pstatus = db.bills.Where(x => x.entry_id == id).ToList();
            if (pstatus.Count > 1)
            {
                TempData["payStatus2"] = "already paid";
                return Json(new { success = false, message = "Already paid" }, JsonRequestBehavior.AllowGet);
            }
            var area = db.Areas.Where(x => x.id == a).FirstOrDefault();
            var areaDetail = db.Area_details.Where(x => x.Area_id == area.id).FirstOrDefault();

            int? unitCost = 0;
            if (data.Vechical_type == "2")
            {
                unitCost = areaDetail.two_price;
            }
            else if (data.Vechical_type == "4")
            {
                unitCost = areaDetail.four_price;

            }

            var amountT = DateTime.Now.TimeOfDay - data.entry_time;
            var hours = (int)Math.Ceiling(amountT.Value.TotalHours);
            var amount = hours * unitCost;

            var model = new bill
            {
                type = "cash",
                money = amount,
                entry_id = id,
                booking_id = data.booking_id,
            };
            db.bills.Add(model);
            db.SaveChanges();
            TempData["id"]=id;
            TempData["Amount"] = amount;

            return RedirectToAction("Checkout","Payment2");

        }


    }

}