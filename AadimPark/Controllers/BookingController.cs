using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Ajax.Utilities;

namespace AadimPark.Controllers
{

    public class BookingController : Controller
    {

        AadimParkEntities2 db = new AadimParkEntities2();

        [HttpGet]
        public ActionResult Index()
        {
            var users = db.Users.ToList();  // Load Users into memory
            var areas = db.Areas.ToList();  // Load Areas into memory

            var data = new BookingVM
            {
                Bookings = db.bookings
                    .ToList() // Fetch bookings in memory first
                    .Select(b => new BookingVM
                    {
                        id = b.id,
                        VehicleNumber = b.Vechical_number,
                        VehicleType = b.vechical_type,
                        UserId = b.userId,
                        UserName = users.FirstOrDefault(u => u.id == b.userId) != null ? users.FirstOrDefault(u => u.id == b.userId).Username : "Unknown",
                        AreaId = b.Area_id,
                        AreaName = areas.FirstOrDefault(a => a.id == b.Area_id) != null ? areas.FirstOrDefault(a => a.id == b.Area_id).Name : "Unknown",
                        Paid = b.paid,
                        
                    })
                    .ToList() // Convert to list after processing
            };

            return View(data);
        }






        [HttpGet]
        public ActionResult UserBooks()
        {
            int id = Convert.ToInt32(Session["Id"]);




            var data = new BookingVM
            {
                bookingg = db.bookings.Where(u => u.userId == id).ToList(),

            };
            return View(data);

        }
        [HttpGet]
        public ActionResult Booking()
        {
            var area = db.Areas
.Select(c => new SelectListItem
{
    Value = c.id.ToString(),
    Text = c.Name
})
.ToList();


            area.Insert(0, new SelectListItem { Value = "", Text = "Select a Spot" });

            ViewBag.Areas = area;
            return View();
        }

        [HttpPost]
        public ActionResult Booking(booking data)
        {
            var id = Convert.ToInt32(Session["Id"]);


            var user = db.bookings.Where(x => x.userId == id).ToList();
            var gg= db.Users.Where(x => x.id == id).FirstOrDefault();
            if (user.Count > 2 && (gg.Member == null || gg.Member == "NO"))
            {
                return RedirectToAction("Offer");
            }


            var area = db.Areas
.Select(c => new SelectListItem
{
    Value = c.id.ToString(),
    Text = c.Name
})
.ToList();

            

            area.Insert(0, new SelectListItem { Value = "", Text = "Select a Spot" });
            ViewBag.Areas = area;

            try
            {

                using (var context = new AadimParkEntities2())
                {

                    var time = new Time_table
                    {
                        month = data.Time_table.month,
                        day = data.Time_table.day,
                        hour = data.Time_table.hour,
                        month_from = data.Time_table.month_from,
                        month_to = data.Time_table.month_to,
                        day_from = data.Time_table.day_from,
                        day_to = data.Time_table.day_to,
                        week_from = data.Time_table.week_from,
                        week_to = data.Time_table.week_to,

                    };
                    db.Time_table.Add(time);
                    db.SaveChanges();
                    var Data = new booking
                    {
                        Vechical_number = data.Vechical_number,
                        vechical_type = data.vechical_type,
                        userId = (int?)id,
                        Area_id = data.Area_id,
                        Time_table_id = time.id,
                        paid = "NO"



                    };
                    var a = db.Area_details.FirstOrDefault(x => x.Area_id == (data.Area_id));


                    if (data.vechical_type == "2")
                    {
                        if (a.twoWheel_occupied == a.twoWheel_capacity)
                        {
                            return Json(new { Sucess = false, message = "Two-wheel capacity is full" });

                        }
                        else
                        {
                            a.twoWheel_occupied++;
                            db.SaveChanges();
                        }


                    }

                    else if (data.vechical_type == "4")
                    {
                        if (a.fourWheel_occupied == a.fourWheel_capacity)
                        {
                            return Json(new { Sucess = false, message = "foue-wheel capacity is full" });



                        }
                        else
                        {
                            a.fourWheel_occupied++;
                            db.SaveChanges();
                        }

                    }


                    db.bookings.Add(Data);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }









            return Json(new { Sucess = true });
        }



        public ActionResult Cancel(BookingVM info)
        {
            var data = db.bookings.FirstOrDefault(x => x.id == (info.id));
            var data2 = new cancled_booking
            {
                time_table_id = data.Time_table_id,
                userId = data.userId,
                vechical_number = data.Vechical_number,
            };

            db.cancled_booking.Add(data2);
            db.bookings.Remove(data);
            db.SaveChanges();
            return Json(new { Sucess = true, JsonRequestBehavior.AllowGet });
        }
        public ActionResult Pay(BookingVM info)
        {
            var data = db.bookings.FirstOrDefault(x => x.id == (info.id));
            var data2 = new cancled_booking
            {
                time_table_id = data.Time_table_id,
                userId = data.userId,
                vechical_number = data.Vechical_number,
            };

            db.cancled_booking.Add(data2);
            db.bookings.Remove(data);
            db.SaveChanges();
            return Json(new { Sucess = true, JsonRequestBehavior.AllowGet });
        }
        [HttpGet]
        public ActionResult Confirm(int id)
        {
            var data = db.bookings.FirstOrDefault(x => x.id == (id));
            if (data.paid != "NO")
            {
                TempData["payStatus"] = "already paid";
                return RedirectToAction("UserBooks");

            }


            return View();
        }
        public ActionResult Offer()
        {

            return View();

        }
        public ActionResult OfferBuy()
        {
            var a = Convert.ToInt32(Session["Id"]);
            var data = db.Users.FirstOrDefault(x => x.id == (a));
           if( data.Member == "YES")
            {
                TempData["memberStatus"] = "already member";

                return RedirectToAction("Booking");

            }
            return View();

        }






    }

}

