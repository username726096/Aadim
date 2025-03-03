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
    public class Payment3Controller : Controller
    {
        AadimParkEntities2 db = new AadimParkEntities2();


        public ActionResult Checkout()
        {


            var a = Convert.ToInt32(Session["Id"]);
           

            var model = new paymentVM
            {
                ID = a,
                Amount = 50,
                TaxAmount = 0,
                TotalAmount = 50,
                TransactionUuid = Guid.NewGuid().ToString(),
                ProductCode = "EPAYTEST",
                SuccessUrl = "https://localhost:44371/Payment3/Sucess",
                FailureUrl = "https://localhost:44371/Payment3/Fail",
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


            var data = db.Users.FirstOrDefault(x => x.id == (info.ID));
            if (data.Member == "YES")
            {
                TempData["MemberNotification"] = "already member";

                return RedirectToAction("Booking", "Booking");

            }
            data.Member = "YES";
            db.SaveChanges();

            TempData["MemberNotification"] = "sucess";

            return RedirectToAction("Booking", "Booking");

            }

        public ActionResult Fail()
        {
            TempData["MemberNotification"] = "failed";

            return RedirectToAction("Booking", "Offer");
        }

    }
}
