//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AadimPark.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admin_list
    {
        public int id { get; set; }
        public Nullable<int> Admin_id { get; set; }
        public Nullable<int> Area_id { get; set; }
    
        public virtual User User { get; set; }
        public virtual Area Area { get; set; }
    }
}
