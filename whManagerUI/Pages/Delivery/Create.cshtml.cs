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

        [BindProperty]
        public whManagerLIB.Models.Delivery NewDelivery { get; set; } = new whManagerLIB.Models.Delivery()
        {
            Car = new whManagerLIB.Models.Car(),
            User = new whManagerLIB.Models.User(),
            Company = new whManagerLIB.Models.Company(),
            DeliveryItems = new List<DeliveryItem>(),
            Trailer = new Trailer(),
        };
        public List<SelectListItem> carOptions { get; set; } = new List<SelectListItem>();
        public List<whManagerLIB.Models.Car> Cars { get; set; } = new List<whManagerLIB.Models.Car>();
        public List<whManagerLIB.Models.DeliveryItemType> DeliveryItemTypes { get; set; } = new List<whManagerLIB.Models.DeliveryItemType>();
        public List<whManagerLIB.Models.User> Users { get; set; } = new List<whManagerLIB.Models.User>();
        public List<SelectListItem> UserOptions { get; set; } = new List<SelectListItem>();
        public string Token { get; set; }

        public CreateModel(DeliveryItemTypeService deliveryItemTypeService, CarService carService, UserService userService)
        {
            _deliveryItemTypeService = deliveryItemTypeService;
            _carService = carService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGet()
        {

            var Token = HttpContext.GetToken();

            DeliveryItemTypes = await _deliveryItemTypeService.GetDeliveryItemTypes(Token);
            Cars = await _carService.GetCars(Token);
            carOptions = Cars.AsQueryable().Select(c =>
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

            return this.Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var token = HttpContext.GetToken();

            NewDelivery.Car = await _carService.GetCar(NewDelivery.CarId, token);
            NewDelivery.User = await _userService.GetUser(NewDelivery.UserId, token);

            return await this.OnGet();
        }
    }
}