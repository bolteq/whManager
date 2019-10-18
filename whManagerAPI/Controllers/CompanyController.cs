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
    [Authorize]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpGet]
        [Route("api/[controller]/{id}")]

        public async Task<IActionResult> GetCompany([FromRoute] int id)
        {
            var company = await _companyService.GetCompany(id);

            return Ok(company);
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetCompanies()
        {
            var companies = _companyService.GetCompanies();

            return Ok(companies);
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> SetCompany([FromBody] Company company)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newCompany = _companyService.AddCompany(company);

            return Ok(newCompany);
        }

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