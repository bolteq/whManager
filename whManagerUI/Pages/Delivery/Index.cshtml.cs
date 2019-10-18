using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerUI.Helpers;
using whManagerUI.Services;
using LIB = whManagerLIB.Models;

namespace whManagerUI.Pages.Delivery
{
    public class IndexModel : PageModel
    {
        private readonly DeliveryService _deliveryService;
        private readonly DeliveryItemTypeService _deliveryItemTypeService;

        [BindProperty]
        public List<LIB.Delivery> Deliveries { get; set; }

        public IndexModel(DeliveryService deliveryService, DeliveryItemTypeService deliveryItemTypeService)
        {
            _deliveryService = deliveryService;
            _deliveryItemTypeService = deliveryItemTypeService;
        }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.GetToken();
            var bToken = String.IsNullOrEmpty(token);

            if (bToken)
            {
                return RedirectToPage("/User/Login");
            }

            Deliveries = new List<LIB.Delivery>();
            Deliveries = await _deliveryService.GetDeliveries(token);

            foreach(var delivery in Deliveries)
            {
                foreach(var deliveryItem in delivery.DeliveryItems)
                {
                    deliveryItem.ItemType = await _deliveryItemTypeService.GetDeliveryItemType(deliveryItem.ItemTypeId, token);
                }
            }

            return Page();
        }
    }
}