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
    public class DeleteModel : PageModel
    {
        private readonly CompanyService _companyService;

        public DeleteModel(CompanyService companyService)
        {
            _companyService = companyService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var token = HttpContext.GetToken();

            await _companyService.DeleteCompany(id, token);

            return RedirectToPage("/Company/Index");
        }
    }
}