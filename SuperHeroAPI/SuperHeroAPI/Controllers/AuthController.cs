using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("/api/user")]
        public Dictionary<string, string> GetUser()
        {
            //User is a ControllerBase member
            return User.Claims.ToDictionary(x => x.Type, x => x.Value);
        }

        [HttpPost("/api/login")]
        public async Task<IResult> Login(LoginForm form, SignInManager<IdentityUser> signInManager)
        {
            var result = await signInManager.PasswordSignInAsync(form.Username, form.Password, true, false);
            if (result.Succeeded)
                return Results.Ok();
            else
                return Results.BadRequest();
        }

        [HttpPost("/api/register")]
        public async Task<IResult> Register(RegisterForm form, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            if (form.Password != form.PasswordConfirm)
                return Results.BadRequest();

            var user = new IdentityUser(form.Username);
            var createUser = await userManager.CreateAsync(user, form.Password);
            if (!createUser.Succeeded)
            {
                return Results.BadRequest();
            }

            await signInManager.SignInAsync(user, true);
            return Results.Ok();
        }

        [HttpGet("/api/logout"), Authorize]
        public async Task<IResult> Logout(SignInManager<IdentityUser> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
    }

    public class RegisterForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class LoginForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
