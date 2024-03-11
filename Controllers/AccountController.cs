using Finshark_API.DTOs.AppUser;
using Finshark_API.Interfaces;
using Finshark_API.Migrations;
using Finshark_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finshark_API.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenInterface _tokenInterface;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenInterface tokenInterface)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenInterface = tokenInterface;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userExists = await _userManager.FindByEmailAsync(userDto.Email);

            if (userExists != null) return BadRequest("E-Mail is already registered use a different email");

            var appUser = new AppUser()
            {
                Email = userDto.Email,
                UserName = userDto.Email.ToUpper()
            };

        }

    }
}
