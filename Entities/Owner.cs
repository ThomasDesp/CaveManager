using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace CaveManager.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        [DefaultValue(false)]
        public bool IsCGUAccepted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? PhoneNumber3 { get; set; }
        public List<Cave>? Caves { get; set; }

        public Owner()
        {
            IsCGUAccepted = false;
        }

    }
}
