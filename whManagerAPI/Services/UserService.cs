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
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace whManagerAPI.Services
{
    public interface IUserService
    {
        Task<User> Login(string username, string password);
        Task<bool> Register(User user);
        Task<User> GetUser(int id);
        IQueryable<User> GetUsers();
        Task<bool> DeleteUser(int id);
    }
    /// <summary>
    /// Klasa serwisu odpowiedzialnego za operacje na tabeli Users w bazie danych
    /// </summary>
    public class UserService : IUserService
    {
        private readonly WHManagerDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly PasswordCrypter _passwordCrypter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Konstruktor serwisu UserService
        /// </summary>
        /// <param name="context">Kontekt bazy danych</param>
        /// <param name="appSettings">Klasa pomocnicza zawierająca ustawienia</param>
        /// <param name="passwordCrypter">Serwis odpowiedzialny za szyfrowanie haseł</param>
        /// <param name="httpContextAccessor">Interfejs pomocniczy służący do dostępu do HttpContextu z serwisu</param>
        public UserService(WHManagerDbContext context, IOptions<AppSettings> appSettings, PasswordCrypter passwordCrypter, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _passwordCrypter = passwordCrypter;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Login
        /// <summary>
        /// Metoda odpowiedzialna za zalogowanie użytkownika o przesłanej nazwie i haśle
        /// </summary>
        /// <param name="username">nazwa użytkownika</param>
        /// <param name="password">hasło</param>
        /// <returns>Zwraca obiekt user z wygenerowanym tokenem dostępowym</returns>
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
            if (user == null)
            {
                return null;
            }

            //Jeśli istnieje, sprawdź czy hasła są zgodne
            if (_passwordCrypter.AreEqual(password, user.PasswordHash, user.PasswordSalt))
            {
                //Utwórz token zawierający uprawnienia użytkownika oraz przypisz go do użytkownika.
                var tokenDescriptor = new MyTokenDescriptor(_appSettings, user);
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }

            //Przed zwróceniem użytkownika ukryj jego hasło
            user.PasswordHash = null;
            user.PasswordSalt = null;

            return user;
        }
        #endregion
        #region Register
        /// <summary>
        /// Metoda dodająca przesłanego użytkownika do bazy danych
        /// </summary>
        /// <param name="user">Użytkownik do zarejestrowania</param>
        /// <returns></returns>
        public async Task<bool> Register(User user)
        {
            var emailValidator = new EmailAddressAttribute();
            var result = new Result();

            //Pobierz ID firmy użytkownika z kontekstu
            var companyId = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Where(c => c.Type == MyClaims.CompanyId)
                            .Select(c => int.Parse(c.Value))
                            .FirstOrDefault();

            //Sprawdź czy użytkownik rejestrujący jest Spedytorem
            bool isSpedytor = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Any(c => c.Value == "Spedytor");

            //Sprawdź, czy nie istnieje już taki użytkownik
            bool bExists = await _context
                            .Users
                            .AnyAsync(u => u.EmailAddress == user.EmailAddress);

            // Nie pozwól Spedytorowi utworzyć użytkownika:
            // - innego niż kierowca
            // - należącego do innej firmy
            if (isSpedytor && (user.Role != RoleHelper.Kierowca || user.CompanyId != companyId)) return false;

            //Jeśli Email jest nieprawdiłowy zwróć null
            if (!emailValidator.IsValid(user.EmailAddress)) return false;

            //Jeśli użytkownik z takim adresem E-Mail już istnieje, zwróć null
            if (bExists) return false;

            //Jeśli wybrana rola nie istnieje, zwróć null
            if (!await _context.Roles.AnyAsync(r => r.Name == user.Role)) return false;


            //Dodaj użytkownika
            user.PasswordSalt = _passwordCrypter.CreateSalt();
            user.PasswordHash = _passwordCrypter.CreateHash(user.PasswordHash, user.PasswordSalt);
            user.DateCreated = DateTime.Now;

            _context.Users.Attach(user);
            await _context.SaveChangesAsync();

            return true;
        }
        #endregion
        #region GetUser
        /// <summary>
        /// Metoda pobierająca użytkownika o podanym ID z bazy danych
        /// </summary>
        /// <param name="id">ID użytkownika</param>
        /// <returns></returns>
        public async Task<User> GetUser(int id)
        {
            //Pobierz ID firmy użytkownika z kontekstu

            var companyId = _httpContextAccessor.HttpContext.User.Claims
                .Where(c => c.Type == MyClaims.CompanyId)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            //Sprawdź czy użytkownik jest Spedytorem / Kierowcą
            bool isSpedytor = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .Any(c => c.Value == RoleHelper.Spedytor);

            bool isKierowca = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .Any(c => c.Value == RoleHelper.Kierowca);

            //Pobierz nazwę użytkownika
            var username = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .FirstOrDefault();

            //Pobierz użytkownika z bazy danych
            var user = await _context
                .Users
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.Id == id);

            //Jeśli wywoła spedytor, a użytkownik nie należy do jego firmy zwróc null
            if (isSpedytor && user.CompanyId != companyId) return null;

            //Jeśli wywoła kierowca, a użytkownik nie jest nim samym zwróć null
            if (isKierowca && user.EmailAddress != username) return null;

            //Wyczyść password/password hash przed zwróceniem
            user.PasswordHash = null;
            user.PasswordSalt = null;

            return user;
        }
        #endregion
        #region GetUsers
        /// <summary>
        /// Metoda pobierająca użytkowników z bazy danych
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetUsers()
        {
            //Pobierz ID firmy użytkownika z kontekstu

            var companyId = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .Where(c => c.Type == MyClaims.CompanyId)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            //Sprawdź czy użytkownik jest Spedytorem
            bool isSpedytor = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .Any(c => c.Value == "Spedytor");

            switch (isSpedytor)
            {
                case true:
                    var companyUsers = _context
                        .Users
                        .Include(u => u.Company)
                        .Where(u => u.CompanyId == companyId);
                    return companyUsers;
                case false:
                    var allUsers = _context
                        .Users
                        .Include(u => u.Company);
                    return allUsers;
            }

            return null;

        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// Metoda usuwająca użytkownika o podanym ID z bazy danych
        /// </summary>
        /// <param name="id">Id użytkownika do usunięcia z bazy danych</param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(int id)
        {

            bool isSpedytor = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Any(c => c.Value == RoleHelper.Spedytor);

            var user = await _context
                            .Users
                            .FirstOrDefaultAsync(c => c.Id == id);

            var companyId = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Where(c => c.Type == MyClaims.CompanyId)
                            .Select(c => int.Parse(c.Value))
                            .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            //Jeśli użytkownik jest spedytorem i car nie jest z jego firmy, zwróc BadRequst
            if (isSpedytor && companyId != user.CompanyId)
            {
                return false;
            }

            _context
                .Users
                .Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
#endregion
    }
}