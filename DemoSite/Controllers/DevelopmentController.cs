using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using DemoSite.Configurations;

namespace DemoSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentController : ControllerBase
    {
        [HttpPost("UploadFile")]
        public object UploadFile(IFormFile file, DataPathConfig config)
        {
            return new {file, config};
        }

        [HttpPost("Raw")]
        public async Task<object> EchoRaw()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        [HttpPost("Debug/FolderPath")]
        public object Debug()
        {
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var commonApplicationDataFolder =
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var tempFolder = Path.GetTempPath();
            return new { userFolder, applicationDataFolder, commonApplicationDataFolder, tempFolder };
        }
    }
}
