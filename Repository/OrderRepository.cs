using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class OrderRepository
    {
        protected readonly DatabaseContext _databaseContext;

        public OrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var entity = await _databaseContext.Orders.AddAsync(order);
            await _databaseContext.SaveChangesAsync();
            return entity.Entity;
        }
        
        public async Task<List<Order>> GetOrders(Guid userId)
        {
            return await _databaseContext.Orders.Where(x => x.UserId == userId).ToListAsync();
        }
        
        public async Task<List<Order>> GetOrders()
        {
            return await _databaseContext.Orders.ToListAsync();
        }

        public async Task DeleteOrder(Guid orderId)
        {
            var order = await _databaseContext.Orders.FindAsync(orderId);
            if (order is not null)
            {
                _databaseContext.Orders.Remove(order);
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await _databaseContext.Orders.FindAsync(orderId);
        }
    }
}