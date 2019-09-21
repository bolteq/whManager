using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace whManagerLIB.Models
{
    public class Unloading
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public int DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }
    }
}
