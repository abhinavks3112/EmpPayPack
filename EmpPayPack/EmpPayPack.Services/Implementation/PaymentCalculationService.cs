using EmpPayPack.Entity;
using EmpPayPack.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpPayPack.Services.Implementation
{
    public class PaymentCalculationService : IPaymentCalculationService
    {
        public readonly ApplicationDbContext _context;
        private decimal _contractualEarnings;
        private decimal _overtimeHours;
        private const decimal rate_Multiplier_For_Overtime_Rate = 1.50m;

        public PaymentCalculationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if(hoursWorked <= contractualHours)
            {
                _contractualEarnings = hoursWorked * hourlyRate;
            }
            else if(hoursWorked > contractualHours)
            {
                _contractualEarnings = contractualHours * hourlyRate;
            }
            return _contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll() => _context.PaymentRecords.OrderBy(p => p.EmployeeId);

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select(t => new SelectListItem 
            { 
                Text = t.YearOfTax,
                Value = t.Id.ToString()
            });
            return allTaxYear;
        }

        public PaymentRecord GetById(int id) => _context.PaymentRecords.Where(p => p.Id == id).FirstOrDefault();

        public decimal NetPay(decimal totalEarnings, decimal totalDeduction) => totalEarnings - totalDeduction;

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours) => overtimeHours * overtimeRate;

        public decimal OvertimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if (hoursWorked <= contractualHours)
            {
                _overtimeHours = 0.00m;
            }
            else if(hoursWorked > contractualHours)
            {
                _overtimeHours = contractualHours - hoursWorked;
            }
            return _overtimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate) => hourlyRate * rate_Multiplier_For_Overtime_Rate;

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFee)
        => tax + nic + studentLoanRepayment + unionFee;

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
        => overtimeEarnings + contractualEarnings;

        public TaxYear GetTaxYearById(int id) =>  _context.TaxYears.Where(t => t.Id == id).FirstOrDefault();
        
    }
}
