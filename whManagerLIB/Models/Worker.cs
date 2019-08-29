using System.ComponentModel.DataAnnotations;

namespace whManagerLIB.Models
{
    public class Worker
    {
        [Key]
        public int workerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
