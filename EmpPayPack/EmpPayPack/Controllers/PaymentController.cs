﻿using EmpPayPack.Constants;
using EmpPayPack.Entity;
using EmpPayPack.Models;
using EmpPayPack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPayPack.Controllers
{
    #region Explaination
    /*
        1. [Authorize(Roles = "Admin, Manager")] means user can have either Admin or Manager Role
        2.  [Authorize(Roles = "Admin")]
            [Authorize(Roles = "Manager")]
            means that user must have both roles(Admin and Manager) associated with him/her.
    */
    #endregion
    [Authorize(Roles = ConstantsKeys.USER_ROLE_ADMIN + "," + ConstantsKeys.USER_ROLE_MANAGER)]
    public class PaymentController : Controller

    {
        private readonly IPaymentCalculationService _paymentService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaxService _taxService;
        private INationalInsuranceContributionService _contributionService;

        private PaymentRecordDetailViewModel GeneratePaymentRecordDetailModel(int Id)
        {
            var paymentRecord = _paymentService.GetById(Id);

            if (paymentRecord == null)
            {
                return null;
            }

            var model = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = paymentRecord.FullName,
                NINO = paymentRecord.NINO,
                PayDate = paymentRecord.PayDate,
                PayMonth = paymentRecord.PayMonth,
                ContractualHours = paymentRecord.ContractualHours,
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeHours = paymentRecord.OvertimeHours,
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _paymentService.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxCode = paymentRecord.TaxCode,
                TaxYear = paymentRecord.TaxYear,
                SLC = paymentRecord.SLC,
                UnionFee = paymentRecord.UnionFee,
                NIC = paymentRecord.NIC,
                HourlyRate = paymentRecord.HourlyRate,
                OvertimeRate = _paymentService.OvertimeRate(paymentRecord.HourlyRate),
                HoursWorked = paymentRecord.HoursWorked,
                TotalDeductions = paymentRecord.TotalDeductions,
                TotalEarnings = paymentRecord.TotalEarnings,
                NetPayment = paymentRecord.NetPayment
            };

            return model;
        }

        public PaymentController(IPaymentCalculationService paymentService, IEmployeeService employeeService, ITaxService taxService, INationalInsuranceContributionService contributionService)
        {
            _paymentService = paymentService;
            _employeeService = employeeService;
            _taxService = taxService;
            _contributionService = contributionService;

        }

        public IActionResult Index()
        {
            var model = _paymentService.GetAll().Select(p => new PaymentRecordIndexViewModel 
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

            return View(model);
        }

        [Authorize(Roles = ConstantsKeys.USER_ROLE_ADMIN)]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new PaymentRecordCreateViewModel();
            PopulateDataFieldsTaxYearsAndEmployees(model);
            return View(model);
        }

        private void PopulateDataFieldsTaxYearsAndEmployees(PaymentRecordCreateViewModel model)
        {
            model.AllTaxYears = _paymentService.GetAllTaxYear();
            model.AllEmployeesForPayrollProcessing = _employeeService.GetAllEmployeesForPayrollProcessing();
        }

        [Authorize(Roles = ConstantsKeys.USER_ROLE_ADMIN)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentRecordCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (_employeeService.GetById(model.EmployeeId) != null)
                {
                    var overtimeHours = _paymentService.OvertimeHours(model.HoursWorked, model.ContractualHours);
                    var overtimeEarnings = _paymentService.OvertimeEarnings(_paymentService.OvertimeRate(model.HourlyRate), overtimeHours);
                    var contractualEarnings = _paymentService.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate);
                    var totalEarnings = _paymentService.TotalEarnings(overtimeEarnings, contractualEarnings);

                    var tax = _taxService.TaxAmount(totalEarnings);
                    var nIC = _contributionService.NIContribution(totalEarnings);
                    var sLC = _employeeService.StudentLoanRepaymentAmount(model.EmployeeId, totalEarnings);
                    var unionFee = _employeeService.UnionFees(model.EmployeeId);

                    var totalDeduction = _paymentService.TotalDeduction(tax, nIC, sLC, unionFee);
                    var netPayment = _paymentService.NetPay(totalEarnings, totalDeduction);

                    var paymentRecord = new PaymentRecord()
                    {
                        Id = model.Id,
                        EmployeeId = model.EmployeeId,
                        FullName = _employeeService.GetById(model.EmployeeId).FullName,
                        NINO = _employeeService.GetById(model.EmployeeId).NationalInsuranceNumber,
                        PayDate = model.PayDate,
                        PayMonth = model.PayMonth,
                        ContractualHours = model.ContractualHours,
                        ContractualEarnings = contractualEarnings,
                        OvertimeHours = overtimeHours,
                        OvertimeEarnings = overtimeEarnings,
                        Tax = tax,
                        TaxYearId = model.TaxYearId,
                        TaxCode = model.TaxCode,
                        TaxYear = model.TaxYear,
                        SLC = sLC,
                        UnionFee = unionFee,
                        NIC = nIC,
                        HourlyRate = model.HourlyRate,
                        HoursWorked = model.HoursWorked,
                        TotalDeductions = totalDeduction,
                        TotalEarnings = totalEarnings,
                        NetPayment = netPayment                       
                    };

                    await _paymentService.CreateAsync(paymentRecord);
                    return RedirectToAction(nameof(Index));
                }
            }
            PopulateDataFieldsTaxYearsAndEmployees(model);            
            return View();
        }
        
        [HttpGet]
        public IActionResult Detail(int Id)
        {
            var model = GeneratePaymentRecordDetailModel(Id);
            
            if(model == null)
            {
                return NotFound();
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Payslip(int Id)
        {
            var model = GeneratePaymentRecordDetailModel(Id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public IActionResult GeneratePaySlipPdf(int Id)
        {
            var model = GeneratePaymentRecordDetailModel(Id);

            if (model == null)
            {
                return NotFound();
            }

            var payslip = new ViewAsPdf(ConstantsKeys.ACTION_METHOD_NAME_PAYSLIP, model);

            payslip.FileName = ConstantsKeys.FILE_PAYSLIP_PDF_NAME;

            return payslip;
        }
    
    }
}
