using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AadimPark.Models.ViewModels
{
    public class BookingVM
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Vehicle Number is required.")]
        [Display(Name = "Vehicle Number")]
        public string VehicleNumber { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required.")]
        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int? UserId { get; set; }     

        [Required(ErrorMessage = "Area is required.")]
        public string Area { get; set; }
        public string User {  get; set; }
        public string UserName { get; set; }
        public int? AreaId { get; set; }
        public string AreaName { get; set; }
        public string Paid { get; set; }

        public List<BookingVM> Bookings { get; set; }
        public List<booking> bookingg { get; set; }
        public List<Area> AreaBooked { get; set; }




        // public List<timetable> Timetables { get; set; }
        //public  int phone_no { get; set; }


    }
}