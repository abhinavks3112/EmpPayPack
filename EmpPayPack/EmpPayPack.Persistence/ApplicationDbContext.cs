using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using EmpPayPack.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmpPayPack.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PaymentRecord> PaymentRecords { get; set; }
        public DbSet<TaxYear> TaxYears { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
             * Scans a given assembly for all types that implement IEntityTypeConfiguration, and registers each one automatically. 
             * So I don't need to add EmployeeConfiguration and PayRecordConfigurations manually.
             * Once this line of code has been added, we no longer need to remember to add new type configuration registrations to the OnModelCreating method as our model grows.
             */
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
