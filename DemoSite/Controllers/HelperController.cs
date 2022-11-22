using DemoSite.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers;

[Route("api")]
[ApiController]
public class HelperController : ControllerBase
{
    [HttpGet("NoAuthentication")]
    public object NoAuthentication()
    {
        return this.Error(new HttpResponseException("Authentication is required.", StatusCodes.Status401Unauthorized));
    }

    [HttpGet("NoAuthorization")]
    public object NoAuthorization()
    {
        return this.Error(new HttpResponseException("Authorization is required.", StatusCodes.Status403Forbidden));
    }
}