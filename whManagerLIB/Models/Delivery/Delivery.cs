using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace whManagerLIB.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string DeliveryFrom { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int CarId { get; set; }
        public Car Car {get;set;}
        public int? TrailerId { get; set; }
        public Trailer Trailer { get; set; }
        public List<DeliveryItem> DeliveryItems { get; set; }
    }
}
