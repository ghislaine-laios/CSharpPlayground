using System.Text.RegularExpressions;
using DemoSite.Configurations;
using DemoSite.Exceptions;
using DemoSite.Ports;
using DemoSite.Repositories;
using Microsoft.VisualBasic;

namespace DemoSite.Services.File
{
    public interface IAvatarStoreService
    {
        Task<string> Execute(long userId, IFormFile file);
    }
    public class AvatarStoreService : FileServiceBase, IAvatarStoreService
    {
        private static readonly Regex ContentTypeRegex = new Regex(@"^image\/*");

        private readonly IUserRepository _userRepository;

        public AvatarStoreService(FileServiceBaseDependency dependency, IUserRepository userRepository)
            : base(dependency)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Execute(long userId, IFormFile file)
        {
            return await WithTransaction(_ => ExecuteWithoutTransaction(userId, file));
        }


        public async Task<string> ExecuteWithoutTransaction(long userId, IFormFile file)
        {
            ValidateContentType(file);
            if (file.Length > Config.AvatarMaxSize) throw new FileTooLargeException();
            var fileEntry = new Models.Domain.File { UserId = userId, ContentType = file.ContentType };

            var user = await _userRepository.Import(userId);
            var nullableGuid = user.UserData.AvatarId;

            await FileEntryRepository.Export(fileEntry);
            user.UserData.AvatarId = fileEntry.Id;
            await _userRepository.Update(user);
            if (nullableGuid is { } avatarId)
            {
                await Delete(avatarId);
            }
            

            await FileRepository.Export(fileEntry, file);
            return fileEntry.Id.ToString();
        }

        private void ValidateContentType(IFormFile file)
        {
            var contentType = file.ContentType;
            var isMatch = ContentTypeRegex.IsMatch(contentType);
            if (!isMatch) throw new ContentTypeNotMatchException("Only image file is accepted.");
        }

        private async Task Delete(Guid avatarId)
        {
            await FileEntryRepository.Delete(avatarId);
            await FileRepository.Delete(avatarId);
        }
    }
}
