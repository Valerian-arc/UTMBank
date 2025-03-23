using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entites
{
    public class User : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public DateTime LastLogin { get; set; }
        public UserRole userRoles { get; set; }
    }
}
