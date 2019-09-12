using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace whManagerLIB.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        public DateTime DateCreated { get; set; }
    }
}
