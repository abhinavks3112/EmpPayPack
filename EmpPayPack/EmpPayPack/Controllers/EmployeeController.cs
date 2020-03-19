using EmpPayPack.Constants;
using EmpPayPack.Entity;
using EmpPayPack.Models;
using EmpPayPack.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPayPack.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var employees = _employeeService.GetAll().Select(e => new EmployeeIndexViewModel()
            {
                Id = e.Id,
                EmployeeNumber = e.EmployeeNumber,
                FullName = e.FullName,
                Gender = e.Gender,
                ImageUrl = e.ImageUrl,
                DateJoined = e.DateJoined,
                Designation = e.Designation,
                City = e.City

            }).ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new EmployeeCreateViewModel());
        }

        [HttpPost]
        // To Prevent Cross-Site Request Forgery Attacks
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    EmployeeNumber = model.EmployeeNumber,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    Phone = model.Phone,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    Designation = model.Designation,
                    NationalInsuranceNumber = model.NationalInsuranceNumber,
                    PaymentMethod = model.PaymentMethod,
                    UnionMember = model.UnionMember,
                    StudentLoan = model.StudentLoan,
                    Address = model.Address,
                    City = model.City,
                    PostCode = model.PostCode
                };

                if(model.ImageUrl != null && model.ImageUrl.Length > ConstantsKeys.LENGTH_0)
                {
                    var uploadDir = ConstantsKeys.FILE_EMPLOYEE_IMAGE_UPLOAD_DIR;
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);

                    // Get the hosting environment
                    var webRootPath = _hostingEnvironment.WebRootPath;

                    // Generate a unique file by concatenating date time format to filename and its extension 
                    fileName = DateTime.UtcNow.ToString(ConstantsKeys.FILE_EMPLOYEE_IMAGE_NAME_DATE_FORMAT) + fileName + extension;

                    // Generate a complete path for storing image file on server
                    var path = Path.Combine(webRootPath, uploadDir, fileName);

                    // Copy the image and save it on server at the specified path, generated in previous step
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                    // Url to store in database
                    employee.ImageUrl = ConstantsKeys.FORWARD_SLASH + uploadDir + ConstantsKeys.FORWARD_SLASH + fileName;
                }

                await _employeeService.CreateASync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
