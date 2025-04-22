using Microsoft.AspNetCore.Mvc;
using RabbitMQLearn.Dto;
using RabbitMQLearn.Models;
using RabbitMQLearn.Services.Abstractions;

namespace RabbitMQLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(Name = "AddUser")]
        //[Route("api/adduser")]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.UserName,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userService.AddAsync(newUser);

            // Publish the user creation event
            await _userService.Publish(newUser);

            return Ok(newUser);
        }
    }
}
