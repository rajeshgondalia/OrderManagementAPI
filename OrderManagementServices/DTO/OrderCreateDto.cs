using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.DTO
{
    public class OrderCreateDto
    {
        private decimal discountOrderAmount;

        public long CustomerId { get; set; }

        public decimal OrderAmount
        {
            get => discountOrderAmount;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Order amount cannot be negative.");

                discountOrderAmount = value > 1000 ? Common.CalculateOrderAmount(value) : value;
            }
        } 
    }
}
