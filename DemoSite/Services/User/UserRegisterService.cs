using DemoSite.Models;
using DemoSite.Ports;

namespace DemoSite.Services.User
{
    public interface IUserRegisterService
    {
        /**
         * <returns>The user id.</returns>
         * <exception cref="UsernameConflictException">Thrown from the repository.</exception>
         */
        Task<long> Execute(FullUserPayload payload);
    }

    public class UserRegisterService: UserServiceBase, IUserRegisterService 
    {
        public UserRegisterService(IUserRepository repo) : base(repo) { }

        public async Task<long> Execute(FullUserPayload payload)
        {
            var user = payload.ToUser();
            return await _repo.Export(user);
        }
    }
}
