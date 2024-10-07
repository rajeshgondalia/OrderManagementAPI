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
    public class Order_Repository : IOrder_Repository, IDisposable
    {
        private readonly OrderManagementContext _context;
        public Order_Repository(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto.OrderAmount <= 0)
                throw new ArgumentException("OrderAmount must be positive.");

            // Simulate checking if CustomerId exists
            if (CustomerExists(orderCreateDto.CustomerId))
                throw new ArgumentException("CustomerId does not exist.");

            var orderAmount = orderCreateDto.OrderAmount;

            var newOrder = new Order
            {
                CustomerId = orderCreateDto.CustomerId,
                OrderAmount = orderAmount,
                OrderDate = DateTime.UtcNow
            };

            _context.Add(newOrder);
            await _context.SaveChangesAsync();
            return await Task.FromResult(newOrder);
        }

        public async Task<Order> GetOrderByIdAsync(long orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            return await Task.FromResult(order);
        }

        public async Task<Order> UpdateOrderAsync(long orderId, OrderUpdateDto orderUpdateDto)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new KeyNotFoundException("Order not found.");

            order.OrderAmount = orderUpdateDto.OrderAmount;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return await Task.FromResult(order);
        }

        public async Task<bool> DeleteOrderAsync(long orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return await Task.FromResult(false);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        private bool CustomerExists(long customerId)
        {
            var customer = _context.Customers.FirstOrDefault(o => o.CustomerId == customerId);
            if (customer == null)
                return true;
            else
                return false;
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
