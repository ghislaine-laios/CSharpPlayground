using DemoSite.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers;

public static class ControllerExtensions
{
    public static ActionResult Error(this ControllerBase controller, HttpResponseException exception)
    {
        return controller.StatusCode(exception.Code, exception.ToDataObject());
    }
}
