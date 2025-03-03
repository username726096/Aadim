using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AadimPark.Models.ViewModels
{
    public class EntryVM
    {
        public int ID { get; set; }
        public int? booking_id { get; set; }


        public DateTime E_time { get; set; }
        public DateTime Ex_time { get; set; }

        public string V_number { get; set; }

        public string V_type { get; set;}
        public string Status { get; set;}
        public Nullable<int> Area_id { get; set; }

        public List<entry> entries { get; set; }

        public string Area { get; set;}




    }
}