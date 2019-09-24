using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace whManagerLIB.Models
{
    public class DeliveryItem
    {
        [Key]
        public int Id { get; set; }
        public int ItemTypeId { get; set; }
        public float Count { get; set; }
        public DeliveryItemType ItemType { get; set; }
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
    }
}
