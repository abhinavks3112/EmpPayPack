using System;
using System.Collections.Generic;
using System.Text;

namespace EmpPayPack.Services
{
    public interface ITaxService
    {
        public decimal TaxAmount(decimal totalAmount);
    }
}
