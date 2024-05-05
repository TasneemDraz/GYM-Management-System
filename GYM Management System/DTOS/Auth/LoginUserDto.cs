using System.ComponentModel.DataAnnotations;

namespace GYM_Management_System.DTOS.Auth
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
