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
    
    public partial class Time_table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Time_table()
        {
            this.bookings = new HashSet<booking>();
        }
    
        public int id { get; set; }
        public Nullable<int> month { get; set; }
        public string month_from { get; set; }
        public string month_to { get; set; }
        public Nullable<int> week { get; set; }
        public Nullable<int> week_from { get; set; }
        public Nullable<int> week_to { get; set; }
        public Nullable<int> day { get; set; }
        public Nullable<System.DateTime> day_from { get; set; }
        public Nullable<System.DateTime> day_to { get; set; }
        public Nullable<int> hour { get; set; }
        public Nullable<System.TimeSpan> hour_from { get; set; }
        public Nullable<System.TimeSpan> hour_to { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }
    }
}
