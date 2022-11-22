using System.Security.Authentication;
using DemoSite.Exceptions;
using DemoSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers;

public static class ControllerExtensions
{
    public static ActionResult Error(this ControllerBase controller, HttpResponseException exception)
    {
        return controller.StatusCode(exception.Code, exception.ToDataObject());
    }

    public static long GetUserId(this ControllerBase controller)
    {
        var principle = controller.User;
        var idClaim = principle.FindFirst("id");
        if (idClaim == null) throw new AuthenticationException();
        return long.Parse(idClaim.Value);
    }
}