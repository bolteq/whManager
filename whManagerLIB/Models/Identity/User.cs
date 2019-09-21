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
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        public DateTime DateCreated { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
