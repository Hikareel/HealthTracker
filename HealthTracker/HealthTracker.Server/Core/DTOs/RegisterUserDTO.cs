﻿using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Core.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(9)]
        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Passwords do not match.")]
        public string password_confirmation { get; set; }
    }
}
