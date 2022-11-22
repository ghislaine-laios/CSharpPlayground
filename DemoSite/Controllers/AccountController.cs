using System.Security.Claims;
using DemoSite.Exceptions;
using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Services.File;
using DemoSite.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multiformats.Hash.Algorithms;

namespace DemoSite.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("Login")]
    public async Task<object> Login([FromBody] BaseUserPayload payload,
        IUserAuthenticationService authenticationService)
    {
        var (success, id) = await authenticationService.Execute(payload);
        if (!success)
            return this.Error(new HttpResponseException("Username or password is incorrect.",
                StatusCodes.Status401Unauthorized));
        var claimsIdentity = new ClaimsIdentity(new List<Claim>
        {
            new(ClaimTypes.Name, payload.Username),
            new("id", id.ToString()),
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
        var principal = User;
        var username = principal.Identity!.Name!;
        return UserResponse.From(await repository.Import(username));
    }

    [Authorize]
    [HttpGet("Avatar")]
    public async Task<object> GetAvatar(IAvatarQueryService queryService)
    {
        var id = this.GetUserId();
        var result = await queryService.Get(id);
        if (result == null) return NotFound();
        return File(result.Content, result.ContentType);
    }

    [Authorize]
    [HttpPost("Avatar/File")]
    public async Task<object> PostAvatar(IFormFile file, IAvatarStoreService storeService)
    {
        var id = this.GetUserId();
        var uuid = await storeService.Execute(id, file);
        return CreatedAtAction(nameof(GetAvatar), new { Id = uuid });
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
}