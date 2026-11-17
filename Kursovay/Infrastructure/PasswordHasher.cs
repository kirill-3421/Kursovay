using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Kursovay.Infrastructure
{
    public static class PasswordHasher
    {
        /// <summary>
        /// хеширование пароля
        /// </summary>
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return System.Convert.ToBase64String(hash);
        }
    }
}
