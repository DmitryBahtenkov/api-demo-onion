using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dtos;
using WebApplication.Dtos.Order;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/order")]
    public class OrderController : ControllerBase
    {

        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var resp = await _orderService.GetOrder(id);
            if (resp.IsSuccess)
            {
                return Ok(resp.Content);
            }

            return BadRequest(resp.Error);
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetOrders(Guid? userId)
        {
            var resp = await _orderService.GetOrders(userId);
            if (resp.IsSuccess)
            {
                return Ok(resp.Content);
            }

            return BadRequest(resp.Error);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var resp = await _orderService.Create(dto);
            if (resp.IsSuccess)
            {
                return Ok(resp.Content);
            }

            return BadRequest(resp.Error);
        }
        
        
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _orderService.Delete(id);
            if (resp.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(resp.Error);
        }
    }
}