using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ABC
    {
        public string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                return Convert.ToBase64String(hashBytes);
            }

        }

    }
}
