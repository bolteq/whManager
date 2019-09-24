using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerUI.Helpers;
using whManagerUI.Services;

namespace whManagerUI.Pages.DeliveryItemType
{
    public class EditModel : PageModel
    {

        private readonly DeliveryItemTypeService _deliveryItemTypeService;

        [BindProperty]
        public whManagerLIB.Models.DeliveryItemType DeliveryItemType { get; set; }
        public EditModel(DeliveryItemTypeService deliveryItemTypeService)
        {
            _deliveryItemTypeService = deliveryItemTypeService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var token = HttpContext.GetToken();

            DeliveryItemType = await _deliveryItemTypeService.GetDeliveryItemType(id, token);

            return Page();
            
        }

        public async Task<IActionResult> OnPost([FromQuery] int id, [FromForm] whManagerLIB.Models.DeliveryItemType deliveryItemType)
        {
            var token = HttpContext.GetToken();
            var existingType = await _deliveryItemTypeService.GetDeliveryItemType(id, token);

            DeliveryItemType.Id = existingType.Id;

            await _deliveryItemTypeService.SetDeliveryItemType(DeliveryItemType, token);

            return RedirectToPage("/DeliveryItemType/index");

        }
    }
}