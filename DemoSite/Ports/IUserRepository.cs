using DemoSite.Models;

namespace DemoSite.Ports;

public interface IUserRepository
{
    Task<User> Import(long id);
    Task<User> Import(string username);

    /**
     * <exception cref="UsernameConflictException">Thrown when there has been a user with same username in the repository.</exception>
     */
    Task<long> Export(User user);
}