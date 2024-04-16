using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace proyectoFinal.CLogica
{
    public class ClsEncriptador
    {

        public static string CrearContrasena(string password, out string salt)
        {
            byte[] byteDeHash = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteDeHash);
            }
            salt = Convert.ToBase64String(byteDeHash);

            string saltedPassword = password + salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool VerificarContrasena(string password, string hashedPassword, string salt)
        {
            string saltedPassword = password + salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                string hashedInput = Convert.ToBase64String(hashedBytes);

                return hashedInput == hashedPassword;
            }
        }


    }
}
