using EmpPayPack.Constants;
using EmpPayPack.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPayPack.Models
{
    public class PaymentRecordIndexViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        [Display(Name = ConstantsKeys.DISPLAYNAME_FULL_NAME)]
        public string FullName { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_PAY_DATE)]
        public DateTime PayDate { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_PAY_MONTH)]
        public string PayMonth { get; set; }
        public int TaxYearId { get; set; }
        public string TaxYear { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_TOTAL_EARNINGS)]
        public decimal TotalEarnings { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_TOTAL_DEDUCTIONS)]
        public decimal TotalDeductions { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_NET_PAYMENT)]
        public decimal NetPayment { get; set; }
    }
}
