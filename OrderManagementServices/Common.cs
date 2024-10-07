using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices
{
    public static class Common
    {  
        public static decimal CalculateOrderAmount(decimal amount)
        {
            return amount - (amount * 10 / 100);
        }
    }
}
