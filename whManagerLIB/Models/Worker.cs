using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace whManagerLIB.Models
{
    public class Worker
    {
        [Key]
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
