using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using System.Text.RegularExpressions;

namespace CaveManager.Entities
{
    [Table("Owner")]
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public bool IsCGUAccepted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? PhoneNumber3 { get; set; }

        public List<Cave>? Caves { get; set; }
        
        public Owner()
        {

        }

        public static bool IsPasswordValidated (string password)
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
    }
}
