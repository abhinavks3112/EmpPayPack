using EmpPayPack.Constants;
using EmpPayPack.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPayPack.Models
{
    public class PaymentRecordCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_EMPLOYEES)]
        public int EmployeeId { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_FULL_NAME)]
        public string FullName { get; set; }
        public string NINO { get; set; }

        [DataType(DataType.Date), Display(Name = ConstantsKeys.DISPLAYNAME_PAY_DATE)]
        public DateTime PayDate { get; set; } = DateTime.UtcNow;

        [Display(Name = ConstantsKeys.DISPLAYNAME_PAY_MONTH)]
        public string PayMonth { get; set; } = DateTime.Today.Month.ToString();

        [Display(Name = ConstantsKeys.DISPLAYNAME_TAX_YEAR)]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }

        // List of all tax years for populating the list
        public IEnumerable<SelectListItem> AllTaxYears { get; set; }
        
        // List of all employees for payroll processing
        public IEnumerable<SelectListItem> AllEmployeesForPayrollProcessing { get; set; }
        public string TaxCode { get; set; } = ConstantsKeys.DEFAULT_TAX_CODE;

        [Display(Name = ConstantsKeys.DISPLAYNAME_HOURLY_RATE)]
        public decimal HourlyRate { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_HOURS_WORKED)]
        public decimal HoursWorked { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_CONTRACTUAL_HOURS)]
        public decimal ContractualHours { get; set; } = ConstantsKeys.DEFAULT_CONTRACTUAL_HOURS;

        [Display(Name = ConstantsKeys.DISPLAYNAME_CONTRACTUAL_EARNINGS)]
        public decimal ContractualEarnings { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_OVERTIME_HOURS)]
        public decimal OvertimeHours { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_OVERTIME_EARNINGS)]
        public decimal OvertimeEarnings { get; set; }
        public decimal Tax { get; set; }

        // National Insurance Contribution
        public decimal NIC { get; set; }
        // Optional(hence nullable) Union Fee
        [Display(Name = ConstantsKeys.DISPLAYNAME_UNION_FEE)]
        public decimal? UnionFee { get; set; }
        // Optional(hence nullable) Student Loan Company
        // This is another way to declaring nullable variable
        public decimal? SLC { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_TOTAL_EARNINGS)]
        public decimal TotalEarnings { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_TOTAL_DEDUCTIONS)]
        public decimal TotalDeductions { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_NET_PAYMENT)]
        public decimal NetPayment { get; set; }
    }
}
