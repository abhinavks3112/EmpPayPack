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
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private decimal _studenLoanAmount;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateASync(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }
        public Employee GetById(int employeeId)
            => _context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();

        public async Task DeleteAsync(int employeeId)
        {  
            _context.Employees.Remove(GetById(employeeId));
             await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int employeeId)
        {
            _context.Employees.Update(GetById(employeeId));
            await _context.SaveChangesAsync();
        }
        public IEnumerable<Employee> GetAll() => _context.Employees;
        public decimal StudentLoanRepaymentAmount(int id, decimal totalAmount)
        {
            var employee = GetById(id);
            _studenLoanAmount = 0;
            if (employee.StudentLoan == StudentLoan.Yes)
            {
                if(totalAmount > 1750 && totalAmount <= 2000)
                {
                    _studenLoanAmount = 15m;
                }
                else if(totalAmount > 2000 && totalAmount <= 2250)
                {
                    _studenLoanAmount = 38m;
                }
                else if (totalAmount > 2250 && totalAmount <= 2500)
                {
                    _studenLoanAmount = 60m;
                }
                else if (totalAmount > 2500 && totalAmount <= 2250)
                {
                    _studenLoanAmount = 83m;
                }
            }
            return _studenLoanAmount;
        }

        public decimal UnionFees(int id) => GetById(id).UnionMember == UnionMember.Yes ? 10m : 0m;

        public IEnumerable<SelectListItem> GetAllEmployeesForPayrollProcessing()
        => GetAll().Select(emp => new SelectListItem()
        {
            Text = emp.FullName,
            Value = emp.Id.ToString()
        });
    }
}
