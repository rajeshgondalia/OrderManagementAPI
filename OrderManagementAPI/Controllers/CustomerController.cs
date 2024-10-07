using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementServices.DTO;
using OrderManagementServices.Models;
using OrderManagementServices.OrderInterface;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer_Repository _icustomerRepository;

        public CustomerController(ICustomer_Repository icustomerRepository)
        {
            _icustomerRepository = icustomerRepository;
        }

        /// <summary>
        /// POST /api/customers
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateDto customer)
        {
            try
            {
                if (customer == null || string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.Email))
                {
                    return BadRequest("CustomerName and Email are required.");
                }

                if (!new EmailAddressAttribute().IsValid(customer.Email))
                {
                    return BadRequest("Email format is invalid.");
                }

                var createdCustomer = await _icustomerRepository.CreateCustomerAsync(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { customerId = createdCustomer.CustomerId }, createdCustomer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GET /api/customers/{customerId}
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomerById(long customerId)
        {
            try
            {
                var customer = await _icustomerRepository.GetCustomerByIdAsync(customerId);

                if (customer == null)
                {
                    return NotFound();
                }

                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GET /api/customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customers = await _icustomerRepository.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
