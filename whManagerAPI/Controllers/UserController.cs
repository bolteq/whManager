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
        private readonly IUserService _userService;

        public UserController(IUserService userService, WHManagerDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        #region GetUser
        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{id}")]

        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userService.GetUser(id);

            if(user == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }

        #endregion

        #region GetUsers
        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        [Route("api/[controller]")]

        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            
            if(users == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(users);
            }
        }

        #endregion
        #region Login
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
        #endregion Login

        #region Register
        [Authorize(Roles = "Administrator, Spedytor")]
        [HttpPost]
        [Route("api/[controller]/register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var bRegister = await _userService.Register(user);

            if(bRegister)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        #endregion

        #region DeleteUser
        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpDelete]
        [Route("api/[controller]")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var bUserDeleted = await _userService.DeleteUser(id);

            if (bUserDeleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}