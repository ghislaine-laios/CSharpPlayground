using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.User;

public class UserServiceBase: DbContextServiceBase
{
    protected readonly IUserRepository Repo;

    public UserServiceBase(IUserRepository repo, ApplicationDbContext context) : base(context)
    {
        Repo = repo;
    }
}