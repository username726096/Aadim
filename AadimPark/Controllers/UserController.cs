using AadimPark.Models;
using AadimPark.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AadimPark.Controllers
{
    public class UserController : Controller
    {

       AadimParkEntities2 db = new AadimParkEntities2();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        public ActionResult Index()
        {
            var data = new UserVM
            {
                Users = db.Users.ToList(),
          
            };
            return View(data);
          
        }
        public ActionResult Edit( int id)
        {
            var user = db.Users.SingleOrDefault(u => u.id == id);
            if (user == null)
            {
                return HttpNotFound(); 
            }
            byte[] passwordBytes = Convert.FromBase64String(user.Password);
            string decryptedPassword = System.Text.Encoding.UTF8.GetString(passwordBytes);
            user.Password = decryptedPassword;
            return View(user);

        }
        [HttpPost]
        public ActionResult Edit(User info)
        {
            var user = db.Users.SingleOrDefault(u => u.id == info.id);

            if (user == null)
            {
                return HttpNotFound();
            }
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(info.Password);


            string base64Password = Convert.ToBase64String(passwordBytes);
            user.Password = base64Password;
            user.Username = info.Username;
            user.Email = info.Email;
     

            db.SaveChanges();
            return RedirectToAction("Index");   

        }
      
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = db.Users.SingleOrDefault(u => u.id == id);
            db.Users.Remove(user);
            db.SaveChanges();

            var a = new deleted_entry { 
                
                
                
                descrpition = "wrong User"
            };
            db.deleted_entry.Add(a);
            db.SaveChanges();

            return Json(new{ success = true });


        }

     








        [HttpGet]
        public ActionResult Logout()
        {
            TempData["Noti"] = "Session Expired or Logged out.";
            Session["Id"] = null;
            return RedirectToAction("Login", "User");
        }


        [HttpPost]
    

        public ActionResult Login( UserVM info)
            {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(info.Password);


            string password = Convert.ToBase64String(passwordBytes);

            var A = db.Users.Where(x => x.Email.Equals(info.Email) && x.Password.Equals(password)).FirstOrDefault();
            if (A != null)
            {

                Session["Id"] = A.id;
                Session["Role"] = A.Role;


                if (A.Role == "User")
                {
                    return Json(new { success = true,  redirectUrl = "/Dashboard/dashboard" });
                    }
                else if( A.Role == "Superadmin")
                {
                    return Json(new { success = true , redirectUrl = "/ADashboard/dashboard" });
                  }
                else
                {
                    return Json(new { success = true, redirectUrl = "/Admin/dashboard" });

                }


            }
            else
            {

                ViewBag.Notification = "Wrong Credits";
                return View(info);
            }    

      

        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
       

        public ActionResult SignUp(  UserVM info)
        {
            if (db.Users.Any(x => x.Email == info.Email))
            {
                ViewBag.Notification = "Already exists";
                return View();
            }
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(info.Password);

    
            string base64Password = Convert.ToBase64String(passwordBytes);
            var data = new User()
            {
                Username = info.Username,
                Email = info.Email,
                Password = base64Password,
                Role = "User",
                Member= "NO"
            };
            db.Users.Add(data);
            db.SaveChanges();
            Session["Role"] = "User";
            Session["Id"] = data.id;

            return Json(new{ Success = true, redirectUrl = "/Dashboard/dashboard" });








        }
    }
}