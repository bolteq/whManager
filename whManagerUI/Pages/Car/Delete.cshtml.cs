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
    public class DeleteModel : PageModel
    {
        private readonly CarService _carService;
        private SessionHelper sessionHelper { get; set; }

        public DeleteModel(CarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            sessionHelper = new SessionHelper();
            sessionHelper.GetSession(HttpContext);

            await _carService.DeleteCar(id, sessionHelper);

            return RedirectToPage("/Car/Index");
        }
    }
}