using EmpPayPack.Models;
using EmpPayPack.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPayPack.Controllers
{ 
    public class PaymentController : Controller

    {
        private readonly IPaymentCalculationService _paymentService;
        public PaymentController(IPaymentCalculationService paymentService)
        {
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            var paymentRecords = _paymentService.GetAll().Select(p => new PaymentRecordIndexViewModel 
            {
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                Employee = p.Employee,
                FullName = p.FullName,
                PayDate = p.PayDate,
                PayMonth = p.PayMonth,
                TaxYearId = p.TaxYearId,
                TaxYear = _paymentService.GetTaxYearById(p.TaxYearId).YearOfTax,
                TotalEarnings = p.TotalEarnings,
                TotalDeductions = p.TotalDeductions,
                NetPayment = p.NetPayment
            });

            return View(paymentRecords);
        }
    }
}
