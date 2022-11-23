using System.Text;
using ByteSizeLib;
using DemoSite.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevelopmentController : ControllerBase
{
    [HttpPost("UploadFile")]
    public object UploadFile(IFormFile file, DataPathConfig config)
    {
        return new
        {
            file,
            config.FilesHostingPath,
            FullPath = config.FilesHostingDirectory.FullName,
            DemoGuid = Guid.NewGuid().ToString(),
        };
    }

    [HttpPost("Raw")]
    public async Task<object> EchoRaw()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }

    [HttpPost("Debug/FolderPath")]
    public object DebugFolderPath()
    {
        var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var commonApplicationDataFolder =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        var tempFolder = Path.GetTempPath();
        return new { userFolder, applicationDataFolder, commonApplicationDataFolder, tempFolder };
    }

    [HttpPost("Debug/ByteSize")]
    public object DebugByteSize()
    {
        var _4Mib = ByteSize.Parse("4Mib");
        return new { _4Mib };
    }
}