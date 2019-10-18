using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerLIB.Helpers;
using whManagerUI.Helpers;
using whManagerUI.Services;

namespace whManagerUI.Pages.Company
{
    public class AddUserDriverModel : PageModel
    {

        private readonly UserService _userService;

        [BindProperty]
        public whManagerLIB.Models.User User { get; set; }

        public AddUserDriverModel(UserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost([FromQuery] int id)
        {
            var token = HttpContext.GetToken();

            User.CompanyId = id;
            User.Role = RoleHelper.Kierowca;

            await _userService.Register(User, token);

            return RedirectToPage("/user/index");
        }

    }
}