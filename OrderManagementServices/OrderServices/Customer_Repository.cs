using Microsoft.EntityFrameworkCore;
using OrderManagementServices.DTO;
using OrderManagementServices.Models;
using OrderManagementServices.OrderInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.OrderServices
{
    public class Customer_Repository : ICustomer_Repository, IDisposable
    {
        private readonly OrderManagementContext _context;
        public Customer_Repository(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomerAsync(CustomerCreateDto customer)
        {
            var newCustomer = new Customer
            {
                CustomerName = customer.CustomerName,
                Email = customer.Email
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<Customer> GetCustomerByIdAsync(long customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
