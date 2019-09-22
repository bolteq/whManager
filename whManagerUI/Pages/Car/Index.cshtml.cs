using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerUI.Helpers;
using whManagerUI.Services;

namespace whManagerUI.Pages.Car
{
    public class IndexModel : PageModel
    {

        private readonly CarService _carService;

        [BindProperty]
        public List<whManagerLIB.Models.Car> Cars { get; set; }
        public whManagerLIB.Models.Car Car { get; set; }
        public SessionHelper sessionHelper { get; set; }

        public IndexModel(CarService carService)
        {
            _carService = carService;

        }
        public async Task<IActionResult> OnGet()
        {
            sessionHelper = new SessionHelper();
            sessionHelper.GetSession(HttpContext);
            var bToken = String.IsNullOrEmpty(sessionHelper.TokenValue);

            if(bToken)
            {
                return RedirectToPage("/User/Login");
            }
            Cars = new List<whManagerLIB.Models.Car>();


            Cars = await _carService.GetCars(sessionHelper);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromForm] whManagerLIB.Models.Car Car)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            sessionHelper = new SessionHelper();
            sessionHelper.GetSession(HttpContext);
            var bToken = String.IsNullOrEmpty(sessionHelper.TokenValue);

            if (bToken)
            {
                return RedirectToPage("/User/Login");
            }
            await _carService.AddCar(Car, sessionHelper);
            Cars = new List<whManagerLIB.Models.Car>();
            Cars = await _carService.GetCars(sessionHelper);
            return Page();
        }
    }

}