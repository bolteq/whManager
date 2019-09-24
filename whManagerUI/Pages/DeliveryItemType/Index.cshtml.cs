using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerLIB.Helpers;
using whManagerUI.Helpers;
using whManagerUI.Services;

namespace whManagerUI.Pages.DeliveryItemType
{
    public class IndexModel : PageModel
    {

        private readonly DeliveryItemTypeService _deliveryItemTypeService;

        [BindProperty]
        public List<whManagerLIB.Models.DeliveryItemType> DeliveryItemTypes { get; set; }
        public whManagerLIB.Models.DeliveryItemType DeliveryItemType { get; set; }

        public IndexModel(DeliveryItemTypeService deliveryItemTypeService)
        {
            _deliveryItemTypeService = deliveryItemTypeService;

        }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.GetToken();
            var role = HttpContext.GetRole();

            if(role != RoleHelper.Administrator)
            {
                return Unauthorized();
            }

            DeliveryItemTypes = new List<whManagerLIB.Models.DeliveryItemType>();
            DeliveryItemTypes = await _deliveryItemTypeService.GetDeliveryItemTypes(token);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromForm] whManagerLIB.Models.DeliveryItemType DeliveryItemType)
        {

            var token = HttpContext.GetToken();
            var bToken = String.IsNullOrEmpty(token);

            if (bToken)
            {
                return RedirectToPage("/User/Login");
            }
            await _deliveryItemTypeService.SetDeliveryItemType(DeliveryItemType, token);

            return RedirectToPage("/DeliveryItemType/Index");
        }
    }

}