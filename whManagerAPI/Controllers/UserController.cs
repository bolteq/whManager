using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Helpers;
using whManagerAPI.Services;
using whManagerLIB.Helpers;
using whManagerLIB.Models;

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {

        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<IActionResult> Login([FromBody]User userData)
        {
            User user = await _userService.Login(userData.EmailAddress, userData.PasswordHash);

            if(user==null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [Authorize(Roles = "Administrator, Spedytor")]
        [HttpPost]
        [Route("api/[controller]/register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Pobierz ID firmy użytkownika z kontekstu

            var companyId = HttpContext.User.Claims
                .Where(c => c.Type == MyClaims.CompanyId)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            //Sprawdź czy użytkownik jest Spedytorem
            bool isSpedytor = HttpContext.User.Claims.Any(c => c.Value == "Spedytor");

            //Nie pozwól Spedytorowi utworzyć użytkownika:
            // - innego niż kierowca
            // - należącego do innej firmy
            if(isSpedytor && (user.Role != "Kierowca" || user.CompanyId != companyId))
            {
                return BadRequest(Errors.InsertDataFailed);
            }

            var result = await _userService.Register(user);
            if(!result.Status)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok();
            }
            
        }
    }
}