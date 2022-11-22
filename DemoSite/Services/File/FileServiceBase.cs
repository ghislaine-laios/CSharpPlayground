using DemoSite.Configurations;
using DemoSite.Exceptions;
using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.File
{
    public class FileServiceBaseDependency
    {
        public readonly IFileRepository FileRepository;
        public readonly IFileEntryRepository FileEntryRepository;
        public readonly FilesHostingConfig Config;
        public readonly ApplicationDbContext DbContext;

        public FileServiceBaseDependency(IFileRepository fileRepository, IFileEntryRepository fileEntryRepository, FilesHostingConfig config, ApplicationDbContext dbContext)
        {
            FileRepository = fileRepository;
            FileEntryRepository = fileEntryRepository;
            Config = config;
            DbContext = dbContext;
        }
    }
    public class FileServiceBase: DbContextServiceBase
    {
        protected readonly IFileRepository FileRepository;
        protected readonly IFileEntryRepository FileEntryRepository;
        protected readonly FilesHostingConfig Config;

        public FileServiceBase(FileServiceBaseDependency dependency) : base(dependency.DbContext)
        {
            FileRepository = dependency.FileRepository;
            FileEntryRepository = dependency.FileEntryRepository;
            Config = dependency.Config;
        }

        /**
         * <summary>Store a user-upload file whose owner is the upload user.</summary>
         * <param name="userId">The id of the user who upload the content.</param>
         * <param name="file">The file object to store.</param>
         * <param name="maxSize">The max size in byte the file can be.</param>
         * <returns>The GUID of the created resource.</returns>
         */
        protected async Task<string> ExecuteWithoutTransaction(long userId, IFormFile file, long maxSize)
        {
            throw new NotImplementedException();
        }
    }
}
