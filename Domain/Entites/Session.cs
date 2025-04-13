using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entites
{
    public class Session : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        public string CookieString { get; set; }

        [Required]
        public DateTime ExpireTime { get; set; }
    }
}
