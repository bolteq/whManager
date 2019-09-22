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

            //Jeśli model nie jest prawidłowy, zwróć 'Bad Request'
            if(!ModelState.IsValid)
            {
                var result = new Result()
                {
                    Message = Errors.InvalidModelState,
                    Status = false
                };
                return BadRequest(result);
            }

            //Sprawdź czy car już istnieje

            var bExists = await _context
                .Cars
                .AnyAsync(c => c.Id == car.Id);

            //Pobierz ID firmy użytkownika z kontekstu

            var companyId = HttpContext.User.Claims
                .Where(c => c.Type == MyClaims.CompanyId)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            
            if (bExists)
            {
                //Jeśli car istnieje, ale nie należy do firma użytkownika, zwróć BadRequest,
                //W przeciwnym wypadku zezwól na modyfikację
                if(companyId != car.companyId)
                {
                    return BadRequest();
                }
                _context.Cars.Attach(car);
                await _context.SaveChangesAsync();
                var result = new Result()
                {
                    Message = SuccessMessages.ObjectModified,
                    Status = true
                };
                return Ok(result);
            }
            else
            {
                //Jeśli car nie istnieje, dodaj go do bazy danych,
                //Ustawiając companyId na companyId użytkownika z kontekstu
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

        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpDelete]
        [Route("api/[controller]")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            bool isSpedytor = HttpContext.User.Claims.Any(c => c.Value == RoleHelper.Spedytor);

            var car = await _context
                .Cars
                .FirstOrDefaultAsync(c => c.Id == id);

            var companyId = HttpContext.User.Claims
                .Where(c => c.Type == MyClaims.CompanyId)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            //Jeśli car do usunięcia nie istnieje, zwróć BadRequest
            if(car == null)
            {
                return BadRequest();
            }

            //Jeśli użytkownik jest spedytorem i car nie jest z jego firmy, zwróc BadRequst
            if(isSpedytor && companyId != car.companyId)
            {
                return BadRequest();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}