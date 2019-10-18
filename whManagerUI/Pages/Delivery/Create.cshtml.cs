using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using whManagerLIB.Helpers;
using whManagerLIB.Models;
using whManagerUI.Helpers;
using whManagerUI.Services;

namespace whManagerUI.Pages.Delivery
{

    public class CreateModel : PageModel
    {
        private readonly DeliveryItemTypeService _deliveryItemTypeService;
        private readonly CarService _carService;
        private readonly UserService _userService;
        private readonly DeliveryService _deliveryService;

        [BindProperty]
        public whManagerLIB.Models.Delivery NewDelivery { get; set; }
        [BindProperty]
        public DeliveryItem DeliveryItem { get; set; }
        public List<SelectListItem> CarOptions { get; set; } = new List<SelectListItem>();
        public List<whManagerLIB.Models.Car> Cars { get; set; }
        public List<whManagerLIB.Models.DeliveryItemType> DeliveryItemTypes { get; set; } = new List<whManagerLIB.Models.DeliveryItemType>();
        public List<SelectListItem> DeliveryItemTypesOptions { get; set; } = new List<SelectListItem>();
        public List<whManagerLIB.Models.User> Users { get; set; }
        public List<SelectListItem> UserOptions { get; set; } = new List<SelectListItem>();

        public string Token { get; set; }

        public CreateModel(DeliveryItemTypeService deliveryItemTypeService, CarService carService, UserService userService, DeliveryService deliveryService)
        {
            _deliveryItemTypeService = deliveryItemTypeService;
            _carService = carService;
            _userService = userService;
            _deliveryService = deliveryService;
        }

        public async Task<IActionResult> OnGet()
        {

            var Token = HttpContext.GetToken();

            DeliveryItemTypes = await _deliveryItemTypeService.GetDeliveryItemTypes(Token);
            DeliveryItemTypesOptions = DeliveryItemTypes.AsQueryable()
                                .Select(dit =>
                                new SelectListItem
                                {
                                    Value = dit.Id.ToString(),
                                    Text = dit.Name
                                }).ToList();

            Cars = await _carService.GetCars(Token);
            CarOptions = Cars.AsQueryable().Select(c =>
                                new SelectListItem
                                {
                                    Value = c.Id.ToString(),
                                    Text = c.PlateNumber
                                }).ToList();

            Users = await _userService.GetUsers(Token);
            UserOptions = Users.AsQueryable().Select(u =>
                                new SelectListItem
                                {
                                    Value = u.Id.ToString(),
                                    Text = u.EmailAddress
                                }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var token = HttpContext.GetToken();
            NewDelivery.DeliveryItems = new List<DeliveryItem>();
            NewDelivery.DeliveryItems.Add(DeliveryItem);
            
            await _deliveryService.AddDelivery(NewDelivery, token);

            return RedirectToPage("/Delivery/Index");
        }
    }
}