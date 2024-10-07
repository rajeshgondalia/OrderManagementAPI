using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Models
{
    public class OrderManagementContext : DbContext
    {
        public OrderManagementContext(DbContextOptions<OrderManagementContext> options)
            : base(options)
        { 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
