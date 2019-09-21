using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace whManagerLIB.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public int companyId { get; set; }
        public Company Company { get; set; }
    }

}
