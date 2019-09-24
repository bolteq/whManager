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
using whManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly WHManagerDbContext _context;
        private UserService _userService;

        public UserController(UserService userService, WHManagerDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{id}")]

        public IActionResult GetUser([FromRoute] int id)
        {
            //Pobierz ID firmy użytkownika z kontekstu

            var companyId = HttpContext.User.Claims
                .Where(c => c.Type == MyClaims.CompanyId)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            //Sprawdź czy użytkownik jest Spedytorem / Kierowcą
            bool isSpedytor = HttpContext.User.Claims.Any(c => c.Value == RoleHelper.Spedytor);
            bool isKierowca = HttpContext.User.Claims.Any(c => c.Value == RoleHelper.Kierowca);

            var username = HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .FirstOrDefault();
            //Pobierz użytkownika z bazy danych
            var user = _context
                .Users
                .Include(u => u.Company)
                .FirstOrDefault(u => u.Id == id);

            //Jeśli wywoła spedytor, a użytkownik nie należy do jego firmy zwróc Unathorized
            if (isSpedytor && user.CompanyId != companyId)
            {
                return Unauthorized();
            }

            //Jeśli wywoła kierowca, a użytkownik nie jest nim samym zwróć Unathorized
            if (isKierowca && user.EmailAddress != username)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        [Route("api/[controller]")]

        public IActionResult GetUsers()
        {
            //Pobierz ID firmy użytkownika z kontekstu

            var companyId = HttpContext.User.Claims
                .Where(c => c.Type == MyClaims.CompanyId)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            //Sprawdź czy użytkownik jest Spedytorem
            bool isSpedytor = HttpContext
                .User
                .Claims
                .Any(c => c.Value == "Spedytor");

            switch (isSpedytor)
            {
                case true:
                    var companyUsers = _context
                        .Users
                        .Include(u => u.Company)
                        .Where(u => u.CompanyId == companyId);
                    return Ok(companyUsers);
                case false:
                    var allUsers = _context
                        .Users
                        .Include(u => u.Company);
                    return Ok(allUsers);
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<IActionResult> Login([FromBody]User userData)
        {
            User user = await _userService.Login(userData.EmailAddress, userData.PasswordHash);

            if (user == null)
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
            if (isSpedytor && (user.Role != "Kierowca" || user.CompanyId != companyId))
            {
                return BadRequest(Errors.InsertDataFailed);
            }

            var result = await _userService.Register(user);
            if (!result.Status)
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