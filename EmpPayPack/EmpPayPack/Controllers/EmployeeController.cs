using EmpPayPack.Constants;
using EmpPayPack.Entity;
using EmpPayPack.Models;
using EmpPayPack.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private async Task<string> SaveImageOnServer(IFormFile imageUrl)
        {
            var uploadDir = ConstantsKeys.FILE_EMPLOYEE_IMAGE_UPLOAD_DIR;
            var fileName = Path.GetFileNameWithoutExtension(imageUrl.FileName);
            var extension = Path.GetExtension(imageUrl.FileName);

            // Get the hosting environment
            var webRootPath = _hostingEnvironment.WebRootPath;

            // Generate a unique file by concatenating date time format to filename and its extension 
            fileName = DateTime.UtcNow.ToString(ConstantsKeys.FILE_EMPLOYEE_IMAGE_NAME_DATE_FORMAT) + fileName + extension;

            // Generate a complete path for storing image file on server
            var path = Path.Combine(webRootPath, uploadDir, fileName);

            // Copy the image and save it on server at the specified path, generated in previous step
            await imageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

            // Url to store in database
            return ConstantsKeys.FORWARD_SLASH + uploadDir + ConstantsKeys.FORWARD_SLASH + fileName;
        }
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(int? pageNumber)
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

            return View(EmployeeListPagination<EmployeeIndexViewModel>.Create(employees, pageNumber ?? 1, ConstantsKeys.PAGINATION_PAGE_SIZE));
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
                    employee.ImageUrl = await SaveImageOnServer(model.ImageUrl);
                }

                await _employeeService.CreateASync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);
            if(employee ==  null)
            {
                return NotFound();
            }
            var model = new EmployeeEditViewModel() {
                Id = employee.Id,
                EmployeeNumber = employee.EmployeeNumber,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                Email = employee.Email,
                Phone = employee.Phone,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                NationalInsuranceNumber = employee.NationalInsuranceNumber,
                PaymentMethod = employee.PaymentMethod,
                UnionMember = employee.UnionMember,
                StudentLoan = employee.StudentLoan,
                Address = employee.Address,
                City = employee.City,
                PostCode = employee.PostCode
            };
            return View(model);
        }

        [HttpPost]
        // To Prevent Cross-Site Request Forgery Attacks
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);

                if(employee == null)
                {
                    return NotFound();
                }

                employee.EmployeeNumber = model.EmployeeNumber;
                employee.FirstName = model.FirstName;
                employee.MiddleName = model.MiddleName;
                employee.LastName = model.LastName;
                employee.Gender = model.Gender;
                employee.Email = model.Email;
                employee.Phone = model.Phone;
                employee.DOB = model.DOB;
                employee.DateJoined = model.DateJoined;
                employee.Designation = model.Designation;
                employee.NationalInsuranceNumber = model.NationalInsuranceNumber;
                employee.PaymentMethod = model.PaymentMethod;
                employee.UnionMember = model.UnionMember;
                employee.StudentLoan = model.StudentLoan;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.PostCode = model.PostCode;
                
                if(model.ImageUrl != null && model.ImageUrl.Length > ConstantsKeys.LENGTH_0)
                {
                    employee.ImageUrl = await SaveImageOnServer(model.ImageUrl);
                }

                await _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var employee = _employeeService.GetById(id);
            if(employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeDetailViewModel()
            {
                Id = employee.Id,
                EmployeeNumber = employee.EmployeeNumber,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Email = employee.Email,
                Phone = employee.Phone,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                NationalInsuranceNumber = employee.NationalInsuranceNumber,
                PaymentMethod = employee.PaymentMethod,
                UnionMember = employee.UnionMember,
                StudentLoan = employee.StudentLoan,
                Address = employee.Address,
                City = employee.City,
                PostCode = employee.PostCode,
                ImageUrl = employee.ImageUrl
            };
            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeDeleteViewModel()
            {
                Id = employee.Id,
                FullName = employee.FullName
            };
            return View(model);
        }

        [HttpPost]
        // To Prevent Cross-Site Request Forgery Attacks
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
            if(ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);
                if (employee == null)
                {
                    return NotFound();
                }
                await _employeeService.DeleteAsync(model.Id);
                return RedirectToAction(nameof(Index));
            }
            return View();            
        }
    }
}
