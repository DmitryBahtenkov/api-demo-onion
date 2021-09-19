using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dtos;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> GetUser(string login, string password)
        {
            var resp = await _userService.GetUser(login, password);
            if (resp.IsSuccess)
            {
                return Ok(resp.Content);
            }

            return BadRequest(resp.Error);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var resp = await _userService.CreateUser(dto);
            if (resp.IsSuccess)
            {
                return Ok(resp.Content);
            }

            return BadRequest(resp.Error);
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
        {
            var resp = await _userService.UpdatePassword(dto);
            if (resp.IsSuccess)
            {
                return Ok(resp.Content);
            }

            return BadRequest(resp.Error);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _userService.Delete(id);
            if (resp.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(resp.Error);
        }
    }
}