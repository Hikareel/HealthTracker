using AutoMapper;
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
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserRepository userRepository, UserManager<User> userManager, IMapper mapper, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _userRepository.LoginAsync(loginDto);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(loginDto.EmailUserName) ?? await _userManager.FindByEmailAsync(loginDto.EmailUserName);
                    var userDTO = _mapper.Map<SuccessLoginDto>(user);
                    userDTO.Token = await _userRepository.GenerateJwtToken(loginDto);
                    return Ok(userDTO);
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the login process for user {EmailUserName}.", loginDto.EmailUserName);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerUserDto)
        {
            try
            {
                var result = await _userRepository.RegisterUserAsync(registerUserDto);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "User registered successfully" });
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the register process for user {UserName}.", registerUserDto.UserName);
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
