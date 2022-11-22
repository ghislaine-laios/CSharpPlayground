using DemoSite.Models.DTO;
using DemoSite.Ports;

namespace DemoSite.Services.File
{
    public interface IAvatarQueryService
    {
        Task<FileWithContent?> Get(long userId);
    }
    public class AvatarQueryService: FileServiceBase, IAvatarQueryService
    {
        private readonly IUserRepository _userRepository;
        public AvatarQueryService(FileServiceBaseDependency dependency, IUserRepository userRepository) : base(dependency)
        {
            _userRepository = userRepository;
        }

        public async Task<FileWithContent?> Get(long userId)
        {
            return await WithTransaction(async _ => await GetWithoutTransaction(userId));
        }

        private async Task<FileWithContent?> GetWithoutTransaction(long userId)
        {
            var user = await _userRepository.Import(userId);
            var avatarId = user.UserData.AvatarId;
            if (avatarId == null) return null;
            var fileEntry = await FileEntryRepository.Import((Guid)avatarId);
            var bytes = await FileRepository.Import((Guid)avatarId);
            return new FileWithContent { Content = bytes, ContentType = fileEntry.ContentType };
        }
    }
}
