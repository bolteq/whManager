using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace whManagerLIB.Models
{
    public class WorkSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime TimeStart { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime TimeEnd { get; set; }

        [Required]
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }


    }
}
