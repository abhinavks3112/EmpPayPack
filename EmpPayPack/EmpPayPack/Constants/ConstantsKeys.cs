using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPayPack.Constants
{
    public class ConstantsKeys
    {
        #region ------Error Messages--------------

        public const string ERRORMESSAGE_EMPLOYEE_NUMBER_REQUIRED = "Employee Number is required.";
        public const string ERRORMESSAGE_FIRST_NAME_REQUIRED = "First Name is required.";
        public const string ERRORMESSAGE_LAST_NAME_REQUIRED = "Last Name is required.";
        public const string ERRORMESSAGE_DESIGNATION_REQUIRED = "Designation is required.";
        public const string ERRORMESSAGE_NATIONAL_INSURANCE_NUMBER_REQUIRED = "National Insurance Number is required.";
        public const string ERRORMESSAGE_ADDRESS_REQUIRED = "Address is required.";
        public const string ERRORMESSAGE_CITY_REQUIRED = "City is required.";
        public const string ERRORMESSAGE_POST_CODE_REQUIRED = "Post Code is required.";

        #endregion

        #region ------Regular Expressions--------------

        // 3 capital letters followed by 3 numbers
        public const string REGEX_EMPLOYEE_NUMBER = @"^[A-Z]{3,3}[0-9]{3,3}$";

        // First capital letter followed by small or capital letter, can contain quotation mark, dash and space(zero or more number of times
        public const string REGEX_FIRST_NAME_LAST_NAME = @"^[A-Z][a-zA-Z""\s-]*$";

        // UK NINO Format
        public const string REGEX_NATIONAL_INSURANCE_NUMBER = @"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$";

        #endregion

        #region -------Display Name--------------

        public const string DISPLAYNAME_FULL_NAME = "Name";
        public const string DISPLAYNAME_FIRST_NAME = "First Name";
        public const string DISPLAYNAME_MIDDLE_NAME = "Middle Name";
        public const string DISPLAYNAME_LAST_NAME = "Last Name";
        public const string DISPLAYNAME_IMAGE_URL = "Photo";
        public const string DISPLAYNAME_DOB = "Date Of Birth";
        public const string DISPLAYNAME_DATE_JOINED = "Date Joined";
        public const string DISPLAYNAME_NATIONAL_INSURANCE_NUMBER = "NINO";
        public const string DISPLAYNAME_PAYMENT_METHOD = "Payment Method";
        public const string DISPLAYNAME_STUDENT_LOAN= "Student Loan";
        public const string DISPLAYNAME_UNION_MEMBER = "Union Member";
        public const string DISPLAYNAME_ADDRESS = "Address";
        public const string DISPLAYNAME_CITY = "City";
        public const string DISPLAYNAME_POST_CODE = "Post Code";

        public const string DISPLAYNAME_PAY_DATE = "Pay Date";
        public const string DISPLAYNAME_PAY_MONTH = "Pay Month";
        public const string DISPLAYNAME_TOTAL_EARNINGS = "Total Earnings";
        public const string DISPLAYNAME_TOTAL_DEDUCTIONS = "Total Deductions";
        public const string DISPLAYNAME_NET_PAYMENT = "Net Pay";

        #endregion

        #region ------Size Constraints--------------

        public const int LENGTH_150 = 150;
        public const int LENGTH_100 = 100;
        public const int LENGTH_50 = 50;
        public const int LENGTH_2 = 2;
        public const int LENGTH_0 = 0;

        #endregion

        #region ------File Operation-------------- 

        public const string FILE_EMPLOYEE_IMAGE_UPLOAD_DIR = @"images/employee";

        // yyyy - year, mm - minute, ss - seconds, fff - miliseconds
        public const string FILE_EMPLOYEE_IMAGE_NAME_DATE_FORMAT = "yyyymmssfff";

        #endregion

        #region -------Miscellaneous--------------

        public const string SINGLE_SPACE = " ";
        public const string DOT = ".";
        public const string FORWARD_SLASH = "/";

        #endregion
    }
}
