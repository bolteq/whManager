﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerUI.Helpers;
using whManagerUI.Services;

namespace whManagerUI.Pages.Car
{
    public class EditModel : PageModel
    {

        private readonly CarService _carService;
        private SessionHelper sessionHelper { get; set; }

        [BindProperty]
        public whManagerLIB.Models.Car Car { get; set; }
        public EditModel(CarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var token = HttpContext.GetToken();

            Car = await _carService.GetCar(id, token);


            return Page();
            
        }

        public async Task<IActionResult> OnPost([FromQuery] int id, [FromForm] whManagerLIB.Models.Car Car)
        {
            var token = HttpContext.GetToken();
            var existingCar = await _carService.GetCar(id, token);

            Car.companyId = existingCar.companyId;
            Car.Id = existingCar.Id;

            await _carService.AddCar(Car, token);

            return RedirectToPage("/car/index");

        }
    }
}