using System.Text.RegularExpressions;

namespace CaveManager.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public bool IsAged { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Adress { get; set; }
        public List<string>? PhoneNumbers { get; set; }

        public List<Cave>? Caves { get; set; }
        
        public Owner()
        {

        }

        public bool IsPasswordValidated (string password)
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
