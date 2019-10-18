using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using whManagerAPI.Helpers;
using whManagerLIB.Models;
using whManagerLIB.Helpers;
using whManagerAPI.Services;

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetCar([FromRoute] int id)
        {
            var car = await _carService.GetCar(id);

            if (car == null)
            {
                return BadRequest();
            }

            return Ok(car);
        }

        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _carService.GetCars();

            return Ok(cars);
        }


        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {

            //Jeśli model nie jest prawidłowy, zwróć 'Bad Request'
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newCar = await _carService.AddCar(car);

            if (newCar == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(car);
            }
        }

        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpDelete]
        [Route("api/[controller]")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var bCarDeleted = await _carService.DeleteCar(id);

            if (bCarDeleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }

}