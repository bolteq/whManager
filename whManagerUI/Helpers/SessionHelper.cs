using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using whManagerLIB.Models;

namespace whManagerUI.Helpers
{
    public class SessionHelper
    {
        public static string Username = "Username";
        public static string Token = "Token";
        public static string CompanyId = "CompanyId";

        public string UsernameValue { get; set; }
        public string TokenValue { get; set; }
        public string CompanyIdValue { get; set; }
        public static void SetSession(HttpContext httpContext, User user)
        {
            httpContext.Session.SetString(SessionHelper.Username, user.EmailAddress);
            httpContext.Session.SetString(SessionHelper.Token, user.Token);
            httpContext.Session.SetString(SessionHelper.CompanyId, user.CompanyId.ToString());
        }

        public void GetSession(HttpContext httpContext)
        {
            UsernameValue = httpContext.Session.GetString(SessionHelper.Username);
            TokenValue = httpContext.Session.GetString(SessionHelper.Token);
            CompanyIdValue = httpContext.Session.GetString(SessionHelper.CompanyId);

        }
    }
}
