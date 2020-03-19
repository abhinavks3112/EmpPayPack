using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpPayPack.Entity
{
    public class PaymentRecord
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string FullName { get; set; }
        public string NINO { get; set; }
        public DateTime PayDate { get; set; }
        public string PayMonth { get; set; }
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }
        public string TaxCode { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal ContractualHours { get; set; }
        public decimal ContractualEarnings { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal OvertimeEarnings { get; set; }
        public decimal Tax { get; set; }

        // National Insurance Contribution
        public decimal NIC { get; set; }
        // Optional(hence nullable) Union Fee
        public decimal? UnionFee { get; set; }
        // Optional(hence nullable) Student Loan Company
        // This is another way to declaring nullable variable
        public Nullable<decimal> SLC { get; set; }
        public Nullable<decimal> TotalEarnings { get; set; }
        public Nullable<decimal> TotalDeductions { get; set; }
        public Nullable<decimal> NetPayment { get; set; }
    }
}