using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace whManagerAPI.Helpers
{
    public class PasswordCrypter
    {

        protected SHA256 sHA256 = SHA256.Create();
        protected RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();

        public string CreateSalt()
        {
            
            byte[] buffer = new byte[64];
            generator.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public string CreateHash(string password, string salt)
        {
            
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var encryptedPassword = sHA256.ComputeHash(bytes);

            return Convert.ToBase64String(encryptedPassword);
        }

        public bool AreEqual(string plainText, string encryptedPassword, string salt)
        {
            string hash = CreateHash(plainText, salt);
            return hash.Equals(encryptedPassword);
        }
    }
}


