using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace whManagerLIB.Models
{
    public class Trailer
    {
        [Key]
        public int Id { get; set; }
        public string PlateNumers { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
