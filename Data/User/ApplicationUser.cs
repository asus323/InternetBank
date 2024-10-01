using Microsoft.AspNetCore.Identity;

namespace InternetBank.Data.User
{
    public class ApplicationUser :IdentityUser
    {
        // public int Id { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public int NationalCode { get; set; }
        public string BirthDate { get; set; }
        // public double PhoneNumber { get; set; }
        // public string Email { get; set; }
    }
}