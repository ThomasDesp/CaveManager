namespace CaveManager.Entities.DTO
{
    public class DTOOwnerCreation
    {
        public bool IsCGUAccepted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? PhoneNumber3 { get; set; }
    }
}