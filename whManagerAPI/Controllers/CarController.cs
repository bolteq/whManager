using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using whManagerAPI.Helpers;

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class CarController : Controller
    {
        private readonly WHManagerDbContext _context;
        public CarController (WHManagerDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            bool isSpedytor = HttpContext.User.Claims.Any(c => c.Value == "Spedytor");
            switch(isSpedytor)
            {
                case true:
                    return Ok();
                case false:
                    return Ok();
            }
            return BadRequest();
        }
    }

}