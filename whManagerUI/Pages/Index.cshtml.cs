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
        public string token { get; set; }
        public string username { get; set; }
        public void OnGet()
        {
            token = HttpContext.Session.GetString(SessionHelper.Token);
            username = HttpContext.Session.GetString(SessionHelper.Username);
        }
    }
}
