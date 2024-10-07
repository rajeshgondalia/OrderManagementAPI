using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagementAPI.Controllers;
using OrderManagementServices.Models;
using OrderManagementServices.OrderInterface;
using OrderManagementServices.OrderServices;

namespace OrderManagementAPI.Test
{
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomer_Repository> _mockCustomerRepository;
        private readonly CustomerController _controller;

        public CustomerControllerTest()
        {
            _mockCustomerRepository = new Mock<ICustomer_Repository>();
            _controller = new CustomerController(_mockCustomerRepository.Object);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsOkResult_WhenCustomerExists()
        {
            // Arrange
            var customerId = 1L;
            var expectedCustomer = new Customer { CustomerId = customerId, CustomerName = "John Doe" };
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByIdAsync(customerId))
                                   .ReturnsAsync(expectedCustomer);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            var okResult = Assert.IsType<ActionResult<Customer>>(result);
            var returnedCustomer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(expectedCustomer.CustomerId, returnedCustomer.CustomerId);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = 2L;
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByIdAsync(customerId))
                                   .ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetCustomerById_ThrowsException_WhenExceptionOccurs()
        {
            // Arrange
            var customerId = 3L;
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByIdAsync(customerId))
                                   .ThrowsAsync(new System.Exception("Test exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.GetCustomerById(customerId));
        }
    }
}