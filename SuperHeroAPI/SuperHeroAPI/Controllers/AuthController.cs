using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //[HttpGet]
        //public async Task<Dictionary<string,string>> User(ClaimsPrincipal user)
        //{
        //    return user.Claims.ToDictionary(x=>x.Type, x=>x.Value);
        //}

        [HttpPost("/api/login")]
        public async Task<IResult> Login(LoginForm form)
        {
            return Results.SignIn(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim("user_id", Guid.NewGuid().ToString()),
                            new Claim("username", form.Username),
                        },
                        CookieAuthenticationDefaults.AuthenticationScheme
                        )
                    ),
                    properties: new AuthenticationProperties() { IsPersistent = true },
                    authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme
                    );
        }

        [HttpGet("/api/logout"), Authorize]
        public async Task<IResult> Logout()
        {
            return Results.SignOut(authenticationSchemes: new List<string>() { CookieAuthenticationDefaults.AuthenticationScheme });
        }
    }

    public class LoginForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
