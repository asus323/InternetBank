using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetBank.Data;
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
    }
}
