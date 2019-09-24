using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerUI.Helpers;
using whManagerUI.Services;

namespace whManagerUI.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public List<whManagerLIB.Models.User> Users { get; set; }

        public IndexModel(UserService userService)
        {
            _userService = userService;

        }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.GetToken();
            var bToken = String.IsNullOrEmpty(token);

            if (bToken)
            {
                return RedirectToPage("/User/Login");
            }

            Users = new List<whManagerLIB.Models.User>();
            Users = await _userService.GetUsers(token);

            return Page();
        }
    }
}