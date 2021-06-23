using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Dtos;
using WebApplication.Dtos.Order;
using WebApplication.Extensions;
using WebApplication.Models;
using WebApplication.Repository;

namespace WebApplication.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseDto<Order>> Create(CreateOrderDto dto)
        {
            var validResp = dto.Validate();
            if (!string.IsNullOrEmpty(validResp))
            {
                return new ResponseDto<Order>() {Error = validResp};
            }

            var order = new Order()
            {
                Id = Guid.NewGuid(),
                FinishedAt = dto.FinishedAt,
                StartAt = dto.StartAt,
                Name = dto.Name,
                UserId = dto.UserId
            };

            return new ResponseDto<Order>()
            {
                IsSuccess = true,
                Content = await _orderRepository.CreateOrder(order)
            };
        }

        public async Task<ResponseDto<List<Order>>> GetOrders(Guid? userId)
        {
            if (!userId.HasValue)
            {
                return new ResponseDto<List<Order>>()
                {
                    IsSuccess = true,
                    Content = await _orderRepository.GetOrders()
                };
            }
            return new ResponseDto<List<Order>>()
            {
                IsSuccess = true,
                Content = await _orderRepository.GetOrders(userId.Value)
            };
        }

        public async Task<ResponseDto<Order>> GetOrder(Guid id)
        {
            return new ResponseDto<Order>()
            {
                IsSuccess = true,
                Content = await _orderRepository.GetOrder(id)
            };
        }
        
        public async Task<ResponseDto> Delete(Guid id)
        {
            await _orderRepository.DeleteOrder(id);
            return new ResponseDto()
            {
                IsSuccess = true,
            };
        }
    }
}