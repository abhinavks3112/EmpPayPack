using EmpPayPack.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmpPayPack.Persistence.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            # region -----Required / Mandatory/ Non-Nullable fields----------
            
            builder.Property(e => e.EmployeeNumber)
                 .IsRequired();
            
            #endregion

            #region -----Required / Mandatory/ Non-Nullable fields with size constraint--------
            
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(e => e.NationalInsuranceNumber)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(e => e.Address)
             .IsRequired()
             .HasMaxLength(150);

            builder.Property(e => e.City)
             .IsRequired()
             .HasMaxLength(50);

            builder.Property(e => e.PostCode)
             .IsRequired()
             .HasMaxLength(50);

            #endregion

            #region -----Non-Required fields with size constraint----------

            builder.Property(e => e.MiddleName)
             .HasMaxLength(50);

            builder.Property(e => e.Phone)
                .HasMaxLength(50);
            
            #endregion
        }
    }
}
