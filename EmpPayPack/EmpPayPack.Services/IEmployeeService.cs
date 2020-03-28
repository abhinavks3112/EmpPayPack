using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmpPayPack.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmpPayPack.Services
{
    public interface IEmployeeService
    {
        Task CreateASync(Employee newEmployee);
        Employee GetById(int employeeId);
        Task UpdateAsync(Employee employee);
        Task UpdateAsync(int employeeId);
        Task DeleteAsync(int employeeId);
        decimal UnionFees(int employeeId);
        decimal StudentLoanRepaymentAmount(int id, decimal totalAmount);
        IEnumerable<Employee> GetAll();
        IEnumerable<SelectListItem> GetAllEmployeesForPayrollProcessing();
    }
}
