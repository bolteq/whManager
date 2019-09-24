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
using whManagerLIB.Helpers;

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

                var tokenDescriptor = new MyTokenDescriptor(_appSettings, user);
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }
                                          
            //Przed zwróceniem użytkownika ukryj jego hasło
            user.PasswordHash = null;

            return user;            
        }

        public async Task<Result> Register(User user)
        {
            var emailValidator = new EmailAddressAttribute();
            var result = new Result();


            try
            {
                //Jeśli Email jest nieprawdiłowy zwróć błąd
                if (!emailValidator.IsValid(user.EmailAddress))
                {
                    result.Status = false;
                    result.Message = "Uncorrect E-Mail Address";
                       
                    return result;
                }
                //Jeśli użytkownik z takim adresem E-Mail już istnieje, zwróć błąd
                bool bExists = await _context.Users.AnyAsync(u => u.EmailAddress == user.EmailAddress);
                if (bExists)
                {
                    result.Status = false;
                    result.Message = "User already exists";

                    return result;
                }
                //Jeśli wybrana rola nie istnieje, zwróć błąd
                if (!await _context.Roles.AnyAsync(r => r.Name == user.Role))
                {
                    result.Status = false;
                    result.Message = "Requested role doesn't exist";

                    return result;
                }

                user.PasswordSalt = _passwordCrypter.CreateSalt();
                user.PasswordHash = _passwordCrypter.CreateHash(user.PasswordHash, user.PasswordSalt);
                user.DateCreated = DateTime.Now;

                _context.Users.Attach(user);
                await _context.SaveChangesAsync();

                result.Status = true;
                result.Message = $"User {user.EmailAddress} created";

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