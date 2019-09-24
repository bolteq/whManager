using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Services;
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
        public async Task<IActionResult> Register([FromBody]User userData)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Sprawdź czy użytkownik jest Spedytorem
            bool isSpedytor = HttpContext.User.Claims.Any(c => c.Value == "Spedytor");

            //Nie pozwól Spedytorowi utworzyć użytkownika o wyższych uprawnieniach od kierowcy
            if(isSpedytor && userData.Role != "Kierowca")
            {
                return BadRequest("Nie możesz utworzyć użytkownika o uprawnieniach wyższych niż Kierowca");
            }

            var result = await _userService.Register(userData);
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