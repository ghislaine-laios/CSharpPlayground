using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoSite.Services.User;

public interface IUserRegisterService
{
    /**
         * <returns>The user id.</returns>
         * <exception cref="UsernameConflictException">Thrown from the repository.</exception>
         */
    Task<long> Execute(FullUserPayload payload);
}

public class UserRegisterService : UserServiceBase, IUserRegisterService
{
    public UserRegisterService(IUserRepository repo, ApplicationDbContext context) : base(repo, context)
    {
    }

    public async Task<long> Execute(FullUserPayload payload)
    {
        return await WithTransaction(_ => ExecuteWithoutTransaction(payload));
    }

    private async Task<long> ExecuteWithoutTransaction(FullUserPayload payload)
    {
        var user = payload.ToUser();
        return await Repo.Export(user);

    }
}