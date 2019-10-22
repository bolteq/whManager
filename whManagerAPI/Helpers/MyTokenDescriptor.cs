using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using whManagerLIB.Models;

namespace whManagerAPI.Helpers
{
    /// <summary>
    /// Klasa generująca token dla użytkownika API
    /// </summary>
    public class MyTokenDescriptor : SecurityTokenDescriptor
    {
        private DateTime tokenExpiryTime = DateTime.UtcNow.AddHours(1);
        private List<Claim> Claims { get; set; }

        /// <summary>
        /// Konstruktor klasy MyTokenDescriptor
        /// </summary>
        /// <param name="appSettings">Ustawienia z appsettings.json</param>
        /// <param name="user">Użytkownik dla którego generowany jest token</param>
        public MyTokenDescriptor(AppSettings appSettings, User user)
        {
            Byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Name, user.EmailAddress));
            Claims.Add(new Claim(ClaimTypes.Role, user.Role));
            Claims.Add(new Claim(MyClaims.CompanyId, user.CompanyId.ToString()));

            Subject = new ClaimsIdentity(Claims);
            Expires = tokenExpiryTime;
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
