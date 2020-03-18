using EmpPayPack.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmpPayPack.Persistence.EntityConfigurations
{
    public class PayRecordConfiguration: IEntityTypeConfiguration<PaymentRecord>
    {
        private const string COLUMN_TYPE_MONEY = "Money";
        private const string COLUMN_TYPE_DECIMAL_WITH_PRECISION_18_2 = "decimal(18,2)";
        public void Configure(EntityTypeBuilder<PaymentRecord> builder)
        {
            #region -----Foreign Key and Relationship------------
            
            /*
             * One Employee can have multiple payment records so configuring One To Many relationship between Employee and PaymentRecords respectively
             * Here, Principal: Employee, Dependent: Payment Records, Navigation Property in Payment Records: Employee, Foreign Key: EmployeeId
            */
            builder.HasOne(p => p.Employee)
                .WithMany(e => e.PaymentRecords)
                .HasForeignKey(p => p.EmployeeId);
            /*
            * One Payment Record must have one tax year specified for it so configuring One To One relationship between Payment Record and PaymentRecords respectively
           */
            builder.HasOne(p => p.TaxYear)
                .WithOne().IsRequired();

            #endregion

            #region -----Column Type: Money--------

            builder.Property(p => p.HourlyRate)
                .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.ContractualEarnings)
               .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.OvertimeEarnings)
               .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.Tax)
               .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.NIC)
               .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.UnionFee)
               .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.SLC)
               .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.TotalEarnings)
               .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.TotalDeductions)
               .HasColumnType(COLUMN_TYPE_MONEY);

            builder.Property(p => p.NetPayment)
               .HasColumnType(COLUMN_TYPE_MONEY);

            #endregion

            #region -----Column Type: Decimal with precision-------

            builder.Property(p => p.HoursWorked)
               .HasColumnType(COLUMN_TYPE_DECIMAL_WITH_PRECISION_18_2);

            builder.Property(p => p.ContractualHours)
              .HasColumnType(COLUMN_TYPE_DECIMAL_WITH_PRECISION_18_2);

            builder.Property(p => p.OvertimeHours)
              .HasColumnType(COLUMN_TYPE_DECIMAL_WITH_PRECISION_18_2);

            #endregion
        }
    }
}
