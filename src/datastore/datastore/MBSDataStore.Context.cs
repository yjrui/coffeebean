﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datastore
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class mobilespyEntities : DbContext
    {
        public mobilespyEntities()
            : base("name=mobilespyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<mbs_callset> mbs_callset { get; set; }
        public DbSet<mbs_contactpropertyset> mbs_contactpropertyset { get; set; }
        public DbSet<mbs_contactset> mbs_contactset { get; set; }
        public DbSet<mbs_deviceset> mbs_deviceset { get; set; }
        public DbSet<mbs_filesystemset> mbs_filesystemset { get; set; }
        public DbSet<mbs_sessionset> mbs_sessionset { get; set; }
        public DbSet<mbs_smsset> mbs_smsset { get; set; }
    }
}
