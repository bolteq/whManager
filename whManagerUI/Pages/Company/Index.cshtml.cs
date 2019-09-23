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
    public class IndexModel : PageModel
    {

        private readonly CompanyService _companyService;

        [BindProperty]
        public List<whManagerLIB.Models.Company> Companies { get; set; }
        public whManagerLIB.Models.Company Company { get; set; }

        public IndexModel(CompanyService companyService)
        {
            _companyService = companyService;

        }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.GetToken();
            var bToken = String.IsNullOrEmpty(token);

            if (bToken)
            {
                return RedirectToPage("/User/Login");
            }

            Companies = new List<whManagerLIB.Models.Company>();
            Companies = await _companyService.GetCompanies(token);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromForm] whManagerLIB.Models.Company Company)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = HttpContext.GetToken();
            var bToken = String.IsNullOrEmpty(token);

            if (bToken)
            {
                return RedirectToPage("/User/Login");
            }
            await _companyService.SetCompany(Company, token);
            Companies = new List<whManagerLIB.Models.Company>();
            Companies = await _companyService.GetCompanies(token);
            return Page();
        }
    }

}