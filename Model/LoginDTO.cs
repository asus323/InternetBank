using System.ComponentModel.DataAnnotations;

namespace InternetBank.Model
{
    public class LogInDTO
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}