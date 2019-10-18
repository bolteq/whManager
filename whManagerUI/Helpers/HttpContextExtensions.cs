using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using whManagerLIB.Models;

namespace whManagerUI.Helpers
{
    public static class HttpContextExtensions
    { 
        public static string GetToken(this HttpContext httpContext)
        {
            return httpContext.Session.GetString(SessionHelper.Token);
        }

        public static string GetRole(this HttpContext httpContext)
        {
            return httpContext.Session.GetString(SessionHelper.Role);
        }

        public static string GetCompanyId(this HttpContext httpContext)
        {
            return httpContext.Session.GetString(SessionHelper.CompanyId);
        }

        public static void SetSession(this HttpContext httpContext, User user)
        {
            httpContext.Session.SetString(SessionHelper.Username, user.EmailAddress);
            httpContext.Session.SetString(SessionHelper.Token, user.Token);
            httpContext.Session.SetString(SessionHelper.CompanyId, user.CompanyId.ToString());
            httpContext.Session.SetString(SessionHelper.Role, user.Role);
        }

        public static User GetUserFromSession(this HttpContext httpContext)
        {
            var user = new User()
            {
                EmailAddress = httpContext.Session.GetString(SessionHelper.Username),
                CompanyId = int.Parse(httpContext.Session.GetString(SessionHelper.CompanyId)),
                Role = httpContext.Session.GetString(SessionHelper.Role),
                Token = httpContext.Session.GetString(SessionHelper.Token)
            };

            return user;
        }

    }
}
