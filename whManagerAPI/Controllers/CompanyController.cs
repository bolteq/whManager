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

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly WHManagerDbContext _context;
        public CompanyController(WHManagerDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpGet]
        [Route("api/[controller]/{id}")]

        public async Task<IActionResult> GetCompany([FromRoute] int id)
        {
            var company = await _context
                .Companies
                .FirstOrDefaultAsync(c => c.Id == id);

            return Ok(company);
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetCompanies()
        {
            var companies = _context.Companies;

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

            bool bExists = await _context.Companies.AnyAsync(c => c.Id == company.Id);

            switch(bExists)
            {
                case true:
                    _context.Companies.Update(company);
                    await _context.SaveChangesAsync();
                    break;
                case false:
                    await _context.Companies.AddAsync(company);
                    await _context.SaveChangesAsync();
                    break;
            }

            return Ok(company);
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpDelete]
        [Route("api/[controller]")]
        public async Task<IActionResult> DeleteCompany([FromQuery] int id)
        {

            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);

            if(company == null)
            {
                return BadRequest();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return Ok();
        }
            
    }
}