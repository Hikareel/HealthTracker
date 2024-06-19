using AutoMapper;
using HealthTracker.Server.Core.DTOs;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HealthTracker.Server.Core.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
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
                    userDTO.Token = await _userRepository.GenerateJwtToken(loginDto.EmailUserName);
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

        [HttpGet("login-google")]
        public IActionResult LoginWithGoogle()
        {
            string? redirectUrl = Url.Action(nameof(HandleGoogleLogin), "Auth");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [HttpGet("handle-google-login")]
        public async Task<IActionResult> HandleGoogleLogin()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                _logger.LogError("Error loading external login information.");
                return StatusCode(500, "External server error.");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
                var userDTO = _mapper.Map<SuccessLoginDto>(user);
                userDTO.Token = await _userRepository.GenerateJwtToken(user.Email);
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var userJson = JsonConvert.SerializeObject(userDTO, settings);
                var frontendUrl = $"https://localhost:5174/login-success?user={Uri.EscapeDataString(userJson)}";
                return Redirect(frontendUrl);
            }
            else
            {
                var user = new User
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                    PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone),
                    DateOfCreate = DateTime.UtcNow
                };

                if (DateTime.TryParse(info.Principal.FindFirstValue(ClaimTypes.DateOfBirth), out DateTime dob))
                {
                    user.DateOfBirth = dob;
                }

                var createUserResult = await _userManager.CreateAsync(user);
                if (createUserResult.Succeeded)
                {
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var userDTO = _mapper.Map<SuccessLoginDto>(user);
                    userDTO.Token = await _userRepository.GenerateJwtToken(user.Email);
                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    var userJson = JsonConvert.SerializeObject(userDTO, settings);
                    var frontendUrl = $"https://localhost:5174/login-success?user={Uri.EscapeDataString(userJson)}";
                    return Redirect(frontendUrl);
                }
                else
                {
                    return StatusCode(500, createUserResult.Errors);
                }
            }
        }



    }
}
