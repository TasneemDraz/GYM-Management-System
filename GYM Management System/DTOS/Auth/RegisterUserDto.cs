using System.ComponentModel.DataAnnotations;

namespace GYM_Management_System.DTOS.Auth
{
    public class RegisterUserDto
    {
        [Required]
        public String UserNmae { get; set; }
        [Required]
        public String Password { get; set; }
        [Compare("Password")]
        [Required]
        public String ConfirmPassword { get; set; }

        public String Email { get; set; }
    }
}
