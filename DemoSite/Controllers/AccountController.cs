using System.Security.Claims;
using DemoSite.Exceptions;
using DemoSite.Models;
using DemoSite.Ports;
using DemoSite.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("Login")]
    public async Task<object> Login([FromBody] BaseUserPayload payload, IUserAuthenticationService authenticationService)
    {
        var success = await authenticationService.Execute(payload);
        if (!success)
            return this.Error(new HttpResponseException("Username or password is incorrect.",
                StatusCodes.Status401Unauthorized));
        var claimsIdentity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, payload.Username)
        }, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);
        return Ok();
    }

    [HttpPost("Register")]
    public async Task<object> Register([FromBody] FullUserPayload payload, IUserRegisterService userRegisterService)
    {
        try
        {
            var id = await userRegisterService.Execute(payload);
            return new { Id = id };
        }
        catch (UsernameConflictException)
        {
            return this.Error(new HttpResponseException("Username is conflict.", StatusCodes.Status409Conflict));
        }
    }

    [HttpPost("Logout")]
    public async Task<object> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<object> GetAccount(IUserRepository repository)
    {
        var principal = this.User;
        var username = principal.Identity!.Name!;
        return UserResponse.From(await repository.Import(username));
    }

    public class UserResponse
    {
        public required string Username { get; init; }
        public required UserDataPayload UserData { get; init; }

        public static UserResponse From(FullUserPayload payload)
        {
            return new UserResponse { Username = payload.Username, UserData = payload.UserData };
        }

        public static UserResponse From(User user)
        {
            return new UserResponse { Username = user.Username, UserData = UserDataPayload.From(user.UserData) };
        }
    }

    [Authorize]
    [HttpPost("Avatar/File")]
    public object PostAvatar()
    {
        return new { };
    }
}
