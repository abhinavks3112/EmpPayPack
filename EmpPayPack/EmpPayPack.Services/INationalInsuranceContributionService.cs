using System;
using System.Collections.Generic;
using System.Text;

namespace EmpPayPack.Services
{
    public interface INationalInsuranceContributionService
    {
        public decimal NIContribution(decimal totalAmount);
    }
}
