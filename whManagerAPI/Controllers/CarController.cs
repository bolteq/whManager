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
        [Route("api/[controller]")]
        public async Task<IActionResult> GetCars()
        {
            bool isSpedytor = HttpContext.User.Claims.Any(c => c.Value == "Spedytor");
            switch(isSpedytor)
            {
                case true:
                    var companyId = HttpContext.User.Claims
                        .Where(c => c.Type == "companyId")
                        .Select(c => int.Parse(c.Value))
                        .FirstOrDefault();
                    var companyCars = _context.Cars.Where(c => c.companyId == companyId);
                    return Ok(companyCars);
                case false:
                    var allCars = _context.Cars;
                    return Ok(allCars);
            }
            return BadRequest();
        }
    }

}