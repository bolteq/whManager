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
    /// <summary>
    /// Kontroler API odpowiadający za operacje na użytkownikach
    /// </summary>
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Konstruktor wstrzykujący serwis UserService do komunikacji z bazą danych
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region GetUser
        /// <summary>
        /// Metoda pobierająca użytkownika o podanym ID
        /// </summary>
        /// <param name="id">Id użytkownika</param>
        /// <returns>Wynik Ok oraz dane o użytkowniku jeśli znaleziono użytkownika, BadRequest jeśli nie znaleziono</returns>
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
        /// <summary>
        /// Metoda pobierająca użytkowników z bazy danych
        /// </summary>
        /// <returns>Ok z listą użytkowników jeśli udało się pobrać, BadRequest jeśli nie</returns>
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
        /// <summary>
        /// Metoda HttpPost, przekazuje do serwisu otrzymanego użytkownika celem zalogowania
        /// </summary>
        /// <param name="userData">Użytkownik do zalogowania</param>
        /// <returns>Jeśli się powiedzie, zwraca użytkownika (serwis dodaje token), jeśli nie, zwraca BadRequest</returns>
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
        /// <summary>
        /// Metoda HttpPost przekazująca do serwisu otrzymanego użytkownika w celu dodania do bazy danych
        /// </summary>
        /// <param name="user">Użytkownik do rejestracji</param>
        /// <returns>Ok jeśli udane, BadRequest jeśli nie udane</returns>
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
        /// <summary>
        /// Metoda Delete przesyłająca do serwisu id użytkownika do usunięcia
        /// </summary>
        /// <param name="id">ID użytkownika do usunięcia</param>
        /// <returns>Ok jeśli użytkownik został usunięty, BadRequest jeśli nie</returns>
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