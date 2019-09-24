using System;
using System.Collections.Generic;
using System.Text;

namespace whManagerLIB.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string DeliveryFrom { get; set; }
        public Car Car {get;set;}
        public Trailer Trailer { get; set; }
        public List<DeliveryItem> DeliveryItems { get; set; }
    }
}
