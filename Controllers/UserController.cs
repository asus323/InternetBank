using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetBank.Data;
using InternetBank.Data.User;
using InternetBank.Interfaces;
using InternetBank.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternetBank.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserRepository userRepository,
            ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] SingUpDTO singUpDTO ){
            var result = await _userRepository.SingUp(singUpDTO); 
            if(result.Succeeded) return Ok();
            return BadRequest(result.Errors.Select(x=>x.Description));
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LogInDTO logInDTO ){
            var result = await _userRepository.LogIn(logInDTO); 
            if(string.IsNullOrEmpty(result)) return Unauthorized();
            return Ok(result);
        }
    }
}
