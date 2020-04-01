using System;
using System.Collections.Generic;
using System.Text;

namespace EmpPayPack.Services.Implementation
{
    public class NationalInsuranceContributionService : INationalInsuranceContributionService
    {
        private const decimal NO_NIC_RATE = 0.00m;
        private const decimal NIC_START_PRIMARY_THRESHOLD_LIMIT_PER_MONTH = 719.00m;
        private const decimal NIC_ABOVE_PRIMARY_THRESHOLD_LIMIT_RATE = 0.12m;
        private const decimal NIC_UPPER_EARNING_LIMIT_PER_MONTH = 4167.00m;
        private const decimal NIC_ABOVE_UPPER_EARNING_LIMIT_RATE = 0.02m;

        private decimal NIC;
        public decimal NIContribution(decimal totalAmount)
        {
           if(totalAmount <= NIC_START_PRIMARY_THRESHOLD_LIMIT_PER_MONTH)
            {
                NIC = NO_NIC_RATE;
            }
           else if(totalAmount > NIC_START_PRIMARY_THRESHOLD_LIMIT_PER_MONTH && totalAmount <= NIC_UPPER_EARNING_LIMIT_PER_MONTH)
            {
                NIC = (totalAmount - NIC_START_PRIMARY_THRESHOLD_LIMIT_PER_MONTH) * NIC_ABOVE_PRIMARY_THRESHOLD_LIMIT_RATE;
            }
           else if(totalAmount > NIC_UPPER_EARNING_LIMIT_PER_MONTH)
            {
                NIC = ((NIC_UPPER_EARNING_LIMIT_PER_MONTH - NIC_START_PRIMARY_THRESHOLD_LIMIT_PER_MONTH) * NIC_ABOVE_PRIMARY_THRESHOLD_LIMIT_RATE)
                    + ((totalAmount - NIC_UPPER_EARNING_LIMIT_PER_MONTH) * NIC_ABOVE_UPPER_EARNING_LIMIT_RATE);
            }
            return NIC;
        }
    }
}
