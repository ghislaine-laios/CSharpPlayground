using DemoSite.Models;
using DemoSite.Ports;

namespace DemoSite.Services.User;

public interface IUserAuthenticationService
{
    Task<bool> Execute(BaseUserPayload payload);
}

public class UserAuthenticationService : UserServiceBase, IUserAuthenticationService
{
    public UserAuthenticationService(IUserRepository repo) : base(repo)
    {
    }

    public async Task<bool> Execute(BaseUserPayload payload)
    {
        var actualUser = await _repo.Import(payload.Username);
        return actualUser.ValidatePassword(payload.Password);
    }
}