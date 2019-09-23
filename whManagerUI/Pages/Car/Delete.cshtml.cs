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

        public DeleteModel(CarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var token = HttpContext.GetToken();

            await _carService.DeleteCar(id, token);

            return RedirectToPage("/Car/Index");
        }
    }
}