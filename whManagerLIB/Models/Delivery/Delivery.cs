using System;
using System.Collections.Generic;
using System.Text;

namespace whManagerLIB.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string DeliveryFrom { get; set; }
        public ICollection<Car> Cars {get;set;}
        public ICollection<Trailer> Trailers { get; set; }
        public ICollection<DeliveryItem> DeliveryItems { get; set; }
    }
}
