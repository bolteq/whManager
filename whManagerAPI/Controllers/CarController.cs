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
using whManagerLIB.Models;
using whManagerLIB.Helpers;

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
        public IActionResult GetCars()
        {
            bool isSpedytor = HttpContext.User.Claims.Any(c => c.Value == RoleHelper.Spedytor);
            switch(isSpedytor)
            {
                case true:
                    var companyId = HttpContext.User.Claims
                        .Where(c => c.Type == MyClaims.CompanyId)
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


        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {
            if(!ModelState.IsValid)
            {
                var result = new Result()
                {
                    Message = Errors.InvalidModelState,
                    Status = false
                };
                return BadRequest(result);
            }

            var bExists = await _context
                .Cars
                .AnyAsync(c => c.Id == car.Id);

            if(bExists)
            {
                _context.Cars.Attach(car);
                await _context.SaveChangesAsync();
                var result = new Result()
                {
                    Message = SuccessMessages.ObjectModified,
                    Status = true
                };
                return BadRequest(result);
            }
            else
            {

                var companyId = HttpContext.User.Claims
                    .Where(c => c.Type == MyClaims.CompanyId)
                    .Select(c => int.Parse(c.Value))
                    .FirstOrDefault();

                car.companyId = companyId;

                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                var result = new Result()
                {
                    Message = SuccessMessages.ObjectCreated,
                    Status = true
                };
              
                return Ok(result);
            }


        }
    }

}