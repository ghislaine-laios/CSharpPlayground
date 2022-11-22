using DemoSite.Ports;

namespace DemoSite.Services.User;

public class UserServiceBase
{
    protected readonly IUserRepository _repo;

    public UserServiceBase(IUserRepository repo)
    {
        _repo = repo;
    }
}