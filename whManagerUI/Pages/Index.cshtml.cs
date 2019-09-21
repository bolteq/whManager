using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using whManagerLIB.Models;
using whManagerUI.Helpers;

namespace whManagerUI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public SessionHelper sessionHelper { get; set; }
        public void OnGet()
        {
            sessionHelper = new SessionHelper();
            sessionHelper.GetSession(HttpContext);
        }
    }
}
