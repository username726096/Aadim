using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AadimPark.Controllers
{
    public class PaymentController : Controller
    {
        AadimParkEntities2 db = new AadimParkEntities2();

       
        public ActionResult Checkout(int id)
        {
            var data = db.bookings.FirstOrDefault(x => x.id == (id));
            if (data.paid != "NO")
            {
                TempData["payStatus"] = "already paid";
                return RedirectToAction("UserBooks", "Booking");


            }
            var TT = db.Time_table.FirstOrDefault(x => x.id == (data.Time_table_id));
            var unit = db.Area_details.FirstOrDefault(y => y.Area_id == (data.Area_id));
            int? cost = 1;
            if(data.vechical_type == "2")
            {
                 cost = unit.two_price;


            }
            else
            {
                 cost = unit.four_price;



            }
            var totalhr = TT.hour * TT.month * TT.week;
            var Money = totalhr * cost;

            var model = new paymentVM
            {
                ID=id,
                Amount = Convert.ToDecimal(Money),
                TaxAmount = 0,
                TotalAmount = Convert.ToDecimal(Money),
                TransactionUuid = Guid.NewGuid().ToString(),
                ProductCode = "EPAYTEST",
                SuccessUrl = "https://localhost:44371/Payment/Sucess",
                FailureUrl = "https://localhost:44371/Payment/Fail",
                SignedFieldNames = "total_amount,transaction_uuid,product_code",
                Secret = "8gBm/:&EnhH.1/q"
            };

            TempData["PaymentInfo"] = model;
            TempData.Keep("PaymentInfo");


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
            var info = TempData["PaymentInfo"] as paymentVM;
            if (info == null || info.ID == null)
            {
                return RedirectToAction("Fail");
            }

            var data = db.bookings.FirstOrDefault(x => x.id == (info.ID));
            data.paid = "YES";
            db.SaveChanges();


            return View();
        }
     
        public ActionResult Fail()
        {
            return View();
        }

    }
}
