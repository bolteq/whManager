using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace whManagerAPI.Helpers
{
    /// <summary>
    /// Serwis odpowiedzialny za szyfrowanie haseł
    /// </summary>
    public class PasswordCrypter
    {

        protected SHA256 sHA256 = SHA256.Create();
        protected RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();

        /// <summary>
        /// Metoda tworząca losową sól do dodania do hasła
        /// </summary>
        /// <returns></returns>
        public string CreateSalt()
        {
            
            byte[] buffer = new byte[64];
            generator.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }
        /// <summary>
        /// Metoda zwracająca hash SHA256 na podstawie przesłanego hasła w postaci czystego tekstu oraz soli
        /// </summary>
        /// <param name="password">Hasło w postaci czystego tekstu</param>
        /// <param name="salt">Sól do dodania do hasła</param>
        /// <returns></returns>
        public string CreateHash(string password, string salt)
        {
            
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var encryptedPassword = sHA256.ComputeHash(bytes);

            return Convert.ToBase64String(encryptedPassword);
        }
        /// <summary>
        /// Metoda sprawdzająca, czy przesłane hasło zgadza się z tym w bazie danych
        /// </summary>
        /// <param name="plainText">Hasło w postaci czystego tekstu</param>
        /// <param name="encryptedPassword">Zaszyfrowane hasło z bazy danych</param>
        /// <param name="salt">Sól dodana do hasła</param>
        /// <returns>True jeśli zgodne, False jeśli nie</returns>
        public bool AreEqual(string plainText, string encryptedPassword, string salt)
        {
            string hash = CreateHash(plainText, salt);
            return hash.Equals(encryptedPassword);
        }
    }
}


