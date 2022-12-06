using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace CaveManager.Entities
{
    [Table("Owner")]
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public bool IsAged { get; set; }
        public bool IsFirstConnection { get; set; }
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

    }
}
