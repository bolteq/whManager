using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerUI.Helpers;
using whManagerUI.Services;

namespace whManagerUI.Pages.Company
{
    public class EditModel : PageModel
    {

        private readonly CompanyService _companyService;

        [BindProperty]
        public whManagerLIB.Models.Company Company { get; set; }
        public EditModel(CompanyService companyService)
        {
            _companyService = companyService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var token = HttpContext.GetToken();

            Company = await _companyService.GetCompany(id, token);


            return Page();
        }

        public async Task<IActionResult> OnPost([FromQuery] int id, [FromForm] whManagerLIB.Models.Company Company)
        {
            var token = HttpContext.GetToken();
            var existingCompany = await _companyService.GetCompany(id, token);

            Company.Id = existingCompany.Id;

            await _companyService.SetCompany(Company, token);

            return RedirectToPage("/Company/index");

        }
    }
}