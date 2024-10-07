using OrderManagementServices.DTO;
using OrderManagementServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.OrderInterface
{
    public interface IOrder_Repository
    {
        Task<Order> CreateOrderAsync(OrderCreateDto orderCreateDto);
        Task<Order> GetOrderByIdAsync(long orderId);
        Task<Order> UpdateOrderAsync(long orderId, OrderUpdateDto orderUpdateDto);
        Task<bool> DeleteOrderAsync(long orderId);
    }
}
