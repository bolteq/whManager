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
    public class DeleteModel : PageModel
    {
        private readonly DeliveryItemTypeService _deliveryItemTypeService;

        public DeleteModel(DeliveryItemTypeService deliveryItemTypeService)
        {
            _deliveryItemTypeService = deliveryItemTypeService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var token = HttpContext.GetToken();

            await _deliveryItemTypeService.DeleteDeliveryItemType(id, token);

            return RedirectToPage("/DeliveryItemType/Index");
        }
    }
}