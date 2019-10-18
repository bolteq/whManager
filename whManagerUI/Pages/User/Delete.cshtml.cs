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
    public class DeleteModel : PageModel
    {
        private readonly UserService _userService;

        public DeleteModel(UserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var token = HttpContext.GetToken();

            await _userService.DeleteUser(id, token);

            return RedirectToPage("/User/Index");
        }
    }
}
