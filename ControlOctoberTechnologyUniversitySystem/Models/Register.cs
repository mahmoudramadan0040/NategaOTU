using System.ComponentModel.DataAnnotations;

namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class Register
    {
        [StringLength(60, MinimumLength = 5)]
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [StringLength(60, MinimumLength = 5)]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
