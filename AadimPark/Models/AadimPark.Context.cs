﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AadimParkEntities2 : DbContext
    {
        public AadimParkEntities2()
            : base("name=AadimParkEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Area_details> Area_details { get; set; }
        public virtual DbSet<booking> bookings { get; set; }
        public virtual DbSet<Time_table> Time_table { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<deleted_entry> deleted_entry { get; set; }
        public virtual DbSet<entry> entries { get; set; }
        public virtual DbSet<Admin_list> Admin_list { get; set; }
        public virtual DbSet<cancled_booking> cancled_booking { get; set; }
        public virtual DbSet<bill> bills { get; set; }
    }
}
