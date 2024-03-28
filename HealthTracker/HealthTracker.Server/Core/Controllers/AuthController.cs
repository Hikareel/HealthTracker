using HealthTracker.Server.Core.DTOs;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace HealthTracker.Server.Core.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;


        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userRepository.LoginAsync(loginDto);
            if (result.Succeeded)
            {
                return Ok(new { Token = _userRepository.GenerateJwtToken() });
            }

            return BadRequest(result.Errors);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var result = await _userRepository.RegisterUserAsync(registerUserDto);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

    }
}
