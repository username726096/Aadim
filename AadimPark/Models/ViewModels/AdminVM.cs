using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AadimPark.Models.ViewModels
{
    public class AdminVM
    {

        public int Id { get; set; }
        public int A_Id { get; set; }

        public string Name { get; set; }
        public List<Admin_list> awd { get; set; }

    }
}