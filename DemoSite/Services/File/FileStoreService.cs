namespace DemoSite.Services.File;

public interface IFileStoreService
{
    Task<string> Execute(string username, IFormFile file);
}

public class FileStoreService : IFileStoreService
{
    public Task<string> Execute(string username, IFormFile file)
    {
        throw new NotImplementedException();
    }
}