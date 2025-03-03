using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AadimPark.Controllers
{
    public class Payment2Controller : Controller
    {
        AadimParkEntities2 db = new AadimParkEntities2();


        public ActionResult Checkout(int idd)
        {


            var a = Convert.ToInt32(Session["Area"]);

            var data = db.entries.Where(x => x.id == idd).FirstOrDefault();
            var data2 = data.booking_id;
            if (data2 != null)
            {
                var data3 = db.bookings.Where(x => x.id == data2).FirstOrDefault();

                if (data3.paid == "YES")
                {
                    TempData["payStatus2"] = "already paid";
                    return RedirectToAction("Fail");

                }
            }
            var pstatus = db.bills.Where(x => x.entry_id == idd).ToList();
            if (pstatus.Count > 0)
            {
                TempData["payStatus2"] = "already paid";
                return RedirectToAction("Fail");

            }
            var id = Convert.ToInt32(TempData["Area"]);
            var areaDetails = db.Area_details.Where(x => x.Area_id == a).FirstOrDefault();
            
            int? unit = null;
            if (data.Vechical_type == "2")
            {
                 unit = areaDetails.two_price;

            }
            else
            {
                 unit = areaDetails.four_price;

            }

            var time = data.entry_time - DateTime.Now.TimeOfDay ;
            double roundedHours = (int)Math.Ceiling(time.Value.TotalHours);
            if (roundedHours == 0)
            {
                roundedHours= 1;
            }
            var amt = roundedHours * unit;




            var model = new paymentVM
            {
                ID = idd,
                Amount = Convert.ToDecimal(amt),
                TaxAmount = 0,
                TotalAmount = Convert.ToDecimal(amt),   
                TransactionUuid = Guid.NewGuid().ToString(),
                ProductCode = "EPAYTEST",
                SuccessUrl = "https://localhost:44371/Payment2/Sucess",
                FailureUrl = "https://localhost:44371/Payment2/Fail",
                SignedFieldNames = "total_amount,transaction_uuid,product_code",
                Secret = "8gBm/:&EnhH.1/q"
            };

            Session["PaymentInfo"] = model; 
           

            model.Signature = GenerateSignature(model.TotalAmount, model.TransactionUuid, model.ProductCode, model.Secret);

            return View(model);
        }


        [HttpPost]
        public ActionResult Checkout(paymentVM model)
        {
            return RedirectToAction("Sucess");
        }

        private string GenerateSignature(decimal totalAmount, string transactionUuid, string productCode, string secret)
        {
            string hashString = $"total_amount={totalAmount},transaction_uuid={transactionUuid},product_code={productCode}";
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(hashString));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public ActionResult Sucess()
        {
            paymentVM info = Session["PaymentInfo"] as paymentVM;
            if (info == null || info.ID == null)
            {
                return RedirectToAction("Fail");
            }


            var data = db.entries.FirstOrDefault(x => x.id == (info.ID));
            //var bill = db.bills.FirstOrDefault(x => x.id == (data.id));

            info.TotalAmount = info.TotalAmount == 0 ? 0 : info.TotalAmount;

            var asdf = new bill
            {
                type = "online",
                money = info.TotalAmount,
                entry_id = info.ID,
                booking_id = data.booking_id,

            };
            db.bills.Add(asdf);
            db.SaveChanges();



            TempData["pNotification"] = "sucess"; 

            return RedirectToAction("Exit", "Entry");

        }

        public ActionResult Fail()
        {
            TempData["pNotification"] = "failed";

            return RedirectToAction("Exit","Entry");
        }

    }
}
