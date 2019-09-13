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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace whManagerAPI.Services
{
    public class UserService //: IUserService
    {
        private readonly WHManagerDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly PasswordCrypter _passwordCrypter;

        public UserService(WHManagerDbContext context, IOptions<AppSettings> appSettings, PasswordCrypter passwordCrypter)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _passwordCrypter = passwordCrypter;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = new User();
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            Byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            //Wyszukaj użytkownika o podanym adrese E-Mail
            user = await _context.
                Users.
                FirstOrDefaultAsync(u => u.EmailAddress == username);

            //Zwróć null jeśli taki user nie istnieje
            if(user == null)
            {
                return null;
            }

            //Jeśli istnieje, sprawdź czy hasła są zgodne
            if(_passwordCrypter.AreEqual(password, user.PasswordHash, user.PasswordSalt))
            {
                //Utwórz token zawierający uprawnienia użytkownika oraz przypisz go do użytkownika.

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
            }
                                          
            //Przed zwróceniem użytkownika ukryj jego hasło
            user.PasswordHash = null;

            return user;            
        }

        public async Task<Result> Register(string emailAddress, string password, string role)
        {
            var newUser = new User();
            var emailValidator = new EmailAddressAttribute();
            var result = new Result();


            try
            {
                //Jeśli Email jest nieprawdiłowy zwróć błąd
                if (!emailValidator.IsValid(emailAddress))
                {
                    result.Status = false;
                    result.Message = "Uncorrect E-Mail Address";
                       
                    return result;
                }
                //Jeśli użytkownik z takim adresem E-Mail już istnieje, zwróć błąd
                if (await _context.Users.AnyAsync(u => u.EmailAddress == emailAddress))
                {
                    result.Status = false;
                    result.Message = "User already exists";

                    return result;
                }
                //Jeśli wybrana rola nie istnieje, zwróć błąd
                if (!await _context.Roles.AnyAsync(r => r.Name == role))
                {
                    result.Status = false;
                    result.Message = "Requested role doesn't exist";

                    return result;
                }

                newUser.PasswordSalt = _passwordCrypter.CreateSalt();
                newUser.PasswordHash = _passwordCrypter.CreateHash(password, newUser.PasswordSalt);
                newUser.EmailAddress = emailAddress;
                newUser.DateCreated = DateTime.Now;
                newUser.Role = role;

                _context.Users.Attach(newUser);
                await _context.SaveChangesAsync();

                result.Status = true;
                result.Message = $"User {newUser.EmailAddress} created";

                return result;
            }
            catch(Exception e)
            {
                result.Status = false;
                result.Message = e.Message;

                return result;
            }
        }
    }
}