using HealthTracker.Server.Core.DTOs;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Encodings.Web;

namespace HealthTracker.Server.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto userDto);
        Task<IdentityResult> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> PasswordResetAsync(PasswordReserDto passwordReserDto);
        string GenerateJwtToken();
    }
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto userDto)
        {
            var userExists = await _userManager.FindByEmailAsync(userDto.Email);
            if (userExists != null)
            {
                return IdentityResult.Failed(new IdentityError { Code = "400", Description = $"E-mail '{userDto.Email}' is already taken." });
            }

            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PhoneNumber = userDto.PhoneNumber,
                DateOfBirth = userDto.DateOfBirth,
                DateOfCreate = DateTime.UtcNow
            };

            return await _userManager.CreateAsync(user, userDto.Password);
        }

        public async Task<IdentityResult> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.EmailUserName) != null ?
                await _userManager.FindByNameAsync(loginDto.EmailUserName) : await _userManager.FindByEmailAsync(loginDto.EmailUserName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Code = "404", Description = $"Username or Email doesn\'t exists." });
            }
            //else if (!user.EmailConfirmed)
            //{
            //    return IdentityResult.Failed(new IdentityError { Code = "405", Description = $"Email is not confirmed. Please confirm before login." });
            //}

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Code = "406", Description = $"User wrong credentials" });

            }
        }

        public string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> PasswordResetAsync(PasswordReserDto passwordReserDto)
        {
            var user = await _userManager.FindByEmailAsync(passwordReserDto.Email);

            if (user != null)
            {
                await _emailSender.SendEmailAsync(passwordReserDto.Email, "Reset Password",
               $"Proszę zresetować hasło, klikając tutaj: <a href='{HtmlEncoder.Default.Encode("https://localhost:5173/login/new-pass/" + GenerateJwtToken())}'>link</a>.");
                return IdentityResult.Success;

            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Code = "404", Description = $"Email doesn\'t exists." });
            }
        }
    }
}
