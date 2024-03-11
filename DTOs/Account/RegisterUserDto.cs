using System.ComponentModel.DataAnnotations;

namespace Finshark_API.DTOs.AppUser
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage ="You need email to login dumbass")]
        [EmailAddress]
        public string Email { get; set; }   

        [Required(ErrorMessage = "Password is required")]
        [Compare("ConfirmPassword",ErrorMessage ="The Passwords you just entered donot match")]
        public string Password { get; set; }    
        [Required]
        public string ConfirmPasswrod { get; set; }
    }
}
