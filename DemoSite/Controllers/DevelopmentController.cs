using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DemoSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentController : ControllerBase
    {
        [HttpPost("UploadFile")]
        public object UploadFile(IFormFile file)
        {
            return file;
        }

        [HttpPost("Raw")]
        public async Task<object> EchoRaw()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
