using System.ComponentModel.DataAnnotations;

namespace Finshark_API.DTOs.AppUser
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage ="You need email to login dumbass")]
        [EmailAddress]
        public string Email { get; set; }   

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
