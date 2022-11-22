using DemoSite.Configurations;
using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using Microsoft.EntityFrameworkCore;
using File = DemoSite.Models.Domain.File;

namespace DemoSite.Repositories;

public class LocalFileRepository: IFileRepository
{
    private readonly FilesHostingConfig _config;

    public LocalFileRepository(ApplicationDbContext dbContext, FilesHostingConfig config)
    {
        _config = config;
    }

    public async Task<byte[]> Import(Guid id)
    {
        var filePath = Path.Combine(_config.Directory.FullName, File.GetFileName(id));
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
        return bytes;
    }

    public async Task Export(File fileEntry, IFormFile formFile)
    {
        var filePath = Path.Combine(_config.Directory.FullName, fileEntry.FileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await formFile.CopyToAsync(stream);
    }

    public Task Delete(Guid id)
    {
        var filePath = Path.Combine(_config.Directory.FullName, File.GetFileName(id));
        System.IO.File.Delete(filePath);
        return Task.CompletedTask;
    }
}