﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FastFly.BeckEnd
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FastFlyModelContainer : DbContext
    {
        public FastFlyModelContainer()
            : base("name=FastFlyModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<CarRent> CarRents { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<TestReplacement> TestReplacements { get; set; }
        public virtual DbSet<StayExpense> StayExpenses { get; set; }
        public virtual DbSet<ReckoningDocument> ReckoningDocuments { get; set; }
        public virtual DbSet<OtherOxpense> OtherOxpenses { get; set; }
        public virtual DbSet<LectureReplacement> LectureReplacements { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<AccommodationAbroad> AccommodationAbroads { get; set; }
        public virtual DbSet<ApplyDocument> ApplyDocuments { get; set; }
        public virtual DbSet<DestinationPeriods> DestinationPeriods { get; set; }
    }
}
