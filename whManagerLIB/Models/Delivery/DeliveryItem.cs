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
        [Required]
        public float Count { get; set; }
        [Required]
        public int ItemTypeId { get; set; }

        public DeliveryItemType ItemType { get; set; }
        [Required]
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        public int UnloadingId { get; set; }
        public Unloading Unloading { get; set; }
    }
}
