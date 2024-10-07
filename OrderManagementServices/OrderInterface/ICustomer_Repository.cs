using OrderManagementServices.DTO;
using OrderManagementServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.OrderInterface
{
    public interface ICustomer_Repository
    {
        Task<Customer> CreateCustomerAsync(CustomerCreateDto customer);
        Task<Customer> GetCustomerByIdAsync(long customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
