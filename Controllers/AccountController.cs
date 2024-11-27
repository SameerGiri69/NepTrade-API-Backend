using Finshark_API.DTOs.Account;
using Finshark_API.DTOs.AppUser;
using Finshark_API.Interfaces;
using Finshark_API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
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

                var result = await _userManager.CreateAsync(appUser, userDto.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok("User Created");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                return StatusCode(500, result.Errors);


            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (User.Identity.IsAuthenticated) return BadRequest("You are already logged in");

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            //var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == myUser.UserName.ToLower());

            if (user == null) return Unauthorized("User doesnot exist");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Incorrect password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            var userIdentity = new ClaimsIdentity(claims, "local");

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            AuthenticationProperties prop = new AuthenticationProperties();
            prop.ExpiresUtc = DateTime.UtcNow.AddMinutes(10);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,prop);

            return Ok(
                new UserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenInterface.GenerateToken(user)
                }
            );
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userExists =  await _userManager.FindByEmailAsync(userDto.Email);

            if (userExists == null) return BadRequest("Incorrect Email");



            var signinResult = await _signInManager.CheckPasswordSignInAsync(userExists, userDto.Password, true);

            if(!signinResult.Succeeded)
            {
                return Unauthorized("Incorrect password");
            }

            var deleteResult = await _userManager.DeleteAsync(userExists);
            if(deleteResult.Succeeded) return Ok("User deleted");
            return BadRequest("something went wrong");
            
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return Ok();
            }
            else
            {
                return BadRequest("you are already logged out");
            }


        }



    }
}
