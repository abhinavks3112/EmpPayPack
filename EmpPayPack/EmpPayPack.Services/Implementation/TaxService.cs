using System;
using System.Collections.Generic;
using System.Text;

namespace EmpPayPack.Services.Implementation
{
    public class TaxService : ITaxService
    {
        private const decimal NO_TAX_UPPER_LIMIT_PER_MONTH = 1042.00m;
        private const decimal NO_TAX_RATE = 0.00m;
        private const decimal BASIC_TAX_UPPER_LIMIT_PER_MONTH = 3125.00m;
        private const decimal BASIC_TAX_RATE = 0.20m;
        private const decimal HIGHER_TAX_UPPER_LIMIT_PER_MONTH = 12500.00m;
        private const decimal HIGHER_TAX_RATE = 0.40m;
        private const decimal ADDITIONAL_TAX_RATE = 0.45m;

        private decimal tax;
        private decimal taxRate;
        public decimal TaxAmount(decimal totalAmount)
        {
            if(totalAmount <= NO_TAX_UPPER_LIMIT_PER_MONTH)
            { 
                tax = NO_TAX_RATE;
            }
            else if(totalAmount > NO_TAX_UPPER_LIMIT_PER_MONTH && totalAmount <= BASIC_TAX_UPPER_LIMIT_PER_MONTH)
            {
                tax = (totalAmount - NO_TAX_UPPER_LIMIT_PER_MONTH) * BASIC_TAX_RATE;
            }
            else if (totalAmount > BASIC_TAX_UPPER_LIMIT_PER_MONTH && totalAmount <= HIGHER_TAX_UPPER_LIMIT_PER_MONTH)
            {
                tax = ((BASIC_TAX_UPPER_LIMIT_PER_MONTH - NO_TAX_UPPER_LIMIT_PER_MONTH) * BASIC_TAX_RATE)
                    + ((totalAmount - BASIC_TAX_UPPER_LIMIT_PER_MONTH) * HIGHER_TAX_RATE);
            }
            else if (totalAmount > HIGHER_TAX_UPPER_LIMIT_PER_MONTH)
            {
                tax = ((BASIC_TAX_UPPER_LIMIT_PER_MONTH - NO_TAX_UPPER_LIMIT_PER_MONTH) * BASIC_TAX_RATE)
                    + ((HIGHER_TAX_UPPER_LIMIT_PER_MONTH - BASIC_TAX_UPPER_LIMIT_PER_MONTH) * HIGHER_TAX_RATE)
                    + ((totalAmount - HIGHER_TAX_UPPER_LIMIT_PER_MONTH) * ADDITIONAL_TAX_RATE);
            }
            // Income tax
            return tax;
        }
    }
}
