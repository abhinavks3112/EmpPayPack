using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EmpPayPack.Constants;
using EmpPayPack.Entity;
using Microsoft.AspNetCore.Http;

namespace EmpPayPack.Models
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantsKeys.ERRORMESSAGE_EMPLOYEE_NUMBER_REQUIRED),
            RegularExpression(ConstantsKeys.REGEX_EMPLOYEE_NUMBER)]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = ConstantsKeys.ERRORMESSAGE_FIRST_NAME_REQUIRED),
        StringLength(ConstantsKeys.LENGTH_50, MinimumLength = ConstantsKeys.LENGTH_2),
        Display(Name = ConstantsKeys.DISPLAYNAME_FIRST_NAME),
        RegularExpression(ConstantsKeys.REGEX_FIRST_NAME_LAST_NAME)]
        public string FirstName { get; set; }

        [StringLength(ConstantsKeys.LENGTH_50),
        Display(Name = ConstantsKeys.DISPLAYNAME_MIDDLE_NAME)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = ConstantsKeys.ERRORMESSAGE_FIRST_NAME_REQUIRED),
        StringLength(ConstantsKeys.LENGTH_50, MinimumLength = ConstantsKeys.LENGTH_2),
        Display(Name = ConstantsKeys.DISPLAYNAME_LAST_NAME),
        RegularExpression(ConstantsKeys.REGEX_FIRST_NAME_LAST_NAME)]
        public string LastName { get; set; }
        public string Gender { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_IMAGE_URL)]
        public IFormFile ImageUrl { get; set; }

        [DataType(DataType.Date), Display(Name = ConstantsKeys.DISPLAYNAME_DOB)]
        public DateTime DOB { get; set; }

        [DataType(DataType.Date), Display(Name = ConstantsKeys.DISPLAYNAME_DATE_JOINED)]
        public DateTime DateJoined { get; set; }

        [Required(ErrorMessage = ConstantsKeys.ERRORMESSAGE_DESIGNATION_REQUIRED),
            StringLength(ConstantsKeys.LENGTH_100)]
        public string Designation { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = ConstantsKeys.ERRORMESSAGE_NATIONAL_INSURANCE_NUMBER_REQUIRED),
            StringLength(ConstantsKeys.LENGTH_50),
            Display(Name = ConstantsKeys.DISPLAYNAME_NATIONAL_INSURANCE_NUMBER),
            RegularExpression(ConstantsKeys.REGEX_NATIONAL_INSURANCE_NUMBER)]
        public string NationalInsuranceNumber { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_PAYMENT_METHOD)]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_STUDENT_LOAN)]
        public StudentLoan StudentLoan { get; set; }

        [Display(Name = ConstantsKeys.DISPLAYNAME_UNION_MEMBER)]
        public UnionMember UnionMember { get; set; }

        [Required(ErrorMessage = ConstantsKeys.ERRORMESSAGE_ADDRESS_REQUIRED),
            Display(Name = ConstantsKeys.DISPLAYNAME_PAYMENT_METHOD),
           StringLength(ConstantsKeys.LENGTH_150)
            ]
        public string Address { get; set; }

        [Required(ErrorMessage = ConstantsKeys.ERRORMESSAGE_CITY_REQUIRED),
            Display(Name = ConstantsKeys.DISPLAYNAME_CITY),
            StringLength(ConstantsKeys.LENGTH_50)]
        public string City { get; set; }

        [Required(ErrorMessage = ConstantsKeys.ERRORMESSAGE_POST_CODE_REQUIRED),
            Display(Name = ConstantsKeys.DISPLAYNAME_POST_CODE),
            StringLength(ConstantsKeys.LENGTH_50)]
        public string PostCode { get; set; }
    }
}
