﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hacimehmetli.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class hacimehmetliEntities : DbContext
    {
        public hacimehmetliEntities()
            : base("name=hacimehmetliEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<categorytable> categorytables { get; set; }
        public virtual DbSet<contactform> contactforms { get; set; }
        public virtual DbSet<menu> menus { get; set; }
        public virtual DbSet<orderstatu> orderstatus { get; set; }
        public virtual DbSet<ordertable> ordertables { get; set; }
        public virtual DbSet<producttable> producttables { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<usertable> usertables { get; set; }
    }
}