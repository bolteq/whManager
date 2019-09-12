using System.ComponentModel.DataAnnotations;

namespace whManagerLIB.Models
{
    public class Role
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
