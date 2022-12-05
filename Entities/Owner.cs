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

    }
}
