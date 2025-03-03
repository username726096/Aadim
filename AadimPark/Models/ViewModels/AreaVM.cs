using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AadimPark.Models.ViewModels
{
    public class AreaVM
    {
        public int id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public Nullable<int> Total_capacity { get; set; }


        public Nullable<int> fourWheel_capacity { get; set; }
        public Nullable<int> twoWheel_capacity { get; set; }
        public Nullable<int> fourWheel_occupied { get; set; }
        public Nullable<int> twoWheel_occupied { get; set; }
        public Nullable<int> Area_id { get; set; }  
        public List<Area> Areas { get; set; }
        public List<Area_details> Area_Details { get; set; }
        public Nullable<int> two_price { get; set; }

        public Nullable<int> four_price { get; set; }


        public List<booking> bookings { get; set; }
        public Area Area { get; set; }
        public List<Area_details> AreaDetails { get; set; }

    }
}