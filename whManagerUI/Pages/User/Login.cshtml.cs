using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerUI.Helpers;
using whManagerUI.Services;
using whManagerLIB.Models;

namespace whManagerUI.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;
        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public void OnGet()
        {
        }

        [BindProperty]
        public whManagerLIB.Models.User user { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            user = await _userService.Login(user);
            bool bToken = string.IsNullOrEmpty(user.Token);

            if (bToken)
            {
                return Page();
            }

            HttpContext.Session.SetString(SessionHelper.Username, user.EmailAddress);
            HttpContext.Session.SetString(SessionHelper.Token, user.Token);

            return RedirectToPage("../Index");

        }

    }
}