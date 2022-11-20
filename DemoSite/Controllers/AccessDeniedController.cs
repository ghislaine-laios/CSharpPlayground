using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessDeniedController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Access Denied.";
        }
    }
}
