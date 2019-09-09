using whManagerAPI.Models;
using whManagerLIB.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using whManagerAPI.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace whManagerAPI.Services
{
    //public interface IUserService
    //{
    //    Task<User> Login(string username, string PasswordHash);
    ////}
    public class UserService //: IUserService
    {
        private readonly WHManagerDbContext _context;
        private readonly AppSettings _appSettings;

        public UserService(WHManagerDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<User> Login(string username, string PasswordHash)
        {
            User user = new User();

            user = await _context.
                Users.
                FirstOrDefaultAsync(u => u.EmailAddress == username && u.PasswordHash == PasswordHash);

                if(user == null)
                {
                    return null;
                }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            Byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.PasswordHash = null;

            return user;            
        }
    }
}