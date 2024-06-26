﻿using KarmaMarketplace.Application.Files.Dto;
using KarmaMarketplace.Domain.User.Enums;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.User.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string EmailAddress { get; set; } = null!;
        public UserRoles? Role { get; set; }
    }
    public class GetUserDto
    {
        public Guid? UserId { get; set; }
        public string? Email { get; set; } = null!;
    }

    public class GetListUserDto
    {
        public UserRoles? Role { get; set; }
        public string? Name { get; set; }
    }

    public class WarnUserDto {
        public string Reason { get; set; } = null!; 
        public Guid UserId { get; set; }
    }

    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class UpdateUserDto {
        [Required]
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public UserRoles? Role { get; set; }
        public CreateFileDto? Avatar { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? TelegramId { get; set; }
        public bool? Blocked { get; set; }
    }

    public class DeleteUserDto 
    {
        [Required]
        public Guid UserId { get; set; }
    }

    public class ResetPasswordDto {
        [Required]
        public string Code { get; set; } = null!; 
        [Required]
        [EmailAddress(ErrorMessage = "No email")]
        public string Email { get; set; } = null!;
        [Required]
        public string NewPassword { get; set; } = null!; 
    }

    public class SendResetCodeDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "No email")]
        public string Email { get; set; } = null!; 
        
    }
}
