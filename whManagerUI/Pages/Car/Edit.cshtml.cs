using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace whManagerUI.Pages.Car
{
    public class EditModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/car/Index");
        }
    }
}