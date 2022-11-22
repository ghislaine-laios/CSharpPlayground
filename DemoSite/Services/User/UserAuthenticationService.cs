using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.User;

public interface IUserAuthenticationService
{
    Task<(bool, long)> Execute(BaseUserPayload payload);
}

public class UserAuthenticationService : UserServiceBase, IUserAuthenticationService
{
    public UserAuthenticationService(IUserRepository repo, ApplicationDbContext context) : base(repo, context)
    {
    }

    public async Task<(bool, long)> Execute(BaseUserPayload payload)
    {
        var actualUser = await Repo.Import(payload.Username);
        return (actualUser.ValidatePassword(payload.Password), actualUser.Id);
    }
}