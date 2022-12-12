using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace CaveManager.Entities
{
    public class Password
    {
        public static bool IsPasswordValidated(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasSpecialChars = new Regex(@"[#?!@$ %^&*-]+");
            var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password) && hasLowerChar.IsMatch(password) && hasSpecialChars.IsMatch(password);

            if (isValidated)
                return true;
            else
                return false;
        }

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            //Convert.ToBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string passwordHashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return passwordHashed;
        }

        //public static bool Isconnected(Owner owner)
        //{
        //    var identity = Owner?.Identity as ClaimsIdentity; 
        //    var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier); 
        //    if (idCurrentUser == null) 
        //        return true;
        //    else 
        //        return false;
        //}
    }
}
