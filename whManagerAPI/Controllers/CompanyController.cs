using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Helpers;
using whManagerAPI.Models;
using whManagerLIB.Models;
using Microsoft.EntityFrameworkCore;
using whManagerLIB.Helpers;
using whManagerAPI.Services;

namespace whManagerAPI.Controllers
{

    /// <summary>
    /// Kontroler odpowiedzialny za obsługę zapytań HTTP dotyczących obiektów Company
    /// </summary>
    [Authorize]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Konstruktor kontrolera wstrzykujący serwis CompanyService do komunikacji z bazą danych
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Metoda HttpGet zwracająca obiekt Company o podanym ID
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Obiekt company z bazy danych o podanym ID</returns>
        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpGet]
        [Route("api/[controller]/{id}")]

        public async Task<IActionResult> GetCompany([FromRoute] int id)
        {
            var company = await _companyService.GetCompany(id);

            return Ok(company);
        }

        /// <summary>
        /// Metoda HttpGet zwracająca listę obiektów Company
        /// </summary>
        /// <returns>Lista firm</returns>
        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetCompanies()
        {
            var companies = _companyService.GetCompanies();

            return Ok(companies);
        }

        /// <summary>
        /// Metoda HttpPost przesyłająca do serwisu otrzymany obiekt Company celem aktualizacji/dodania
        /// </summary>
        /// <param name="company">Obiekt do aktualizacji/dodania</param>
        /// <returns>BadRequest - niepowodzenie, Ok - powodzenie</returns>
        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> SetCompany([FromBody] Company company)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newCompany = await _companyService.AddCompany(company);

            return Ok(newCompany);
        }
        /// <summary>
        /// Metoda przesyłająca do serwisu otrzymane id w celu usunięcia obiektu z bazy danych
        /// </summary>
        /// <param name="id">Id obiektu do usunięcia</param>
        /// <returns>BadRequest - niepowodzenie, Ok - powodzenie</returns>
        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpDelete]
        [Route("api/[controller]")]
        public async Task<IActionResult> DeleteCompany([FromQuery] int id)
        {
            if(await _companyService.DeleteCompany(id))
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