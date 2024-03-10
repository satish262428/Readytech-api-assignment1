// AprilFirstHandler.cs
using System;
using Microsoft.AspNetCore.Mvc;

public class AprilFirstHandler : BaseHandler
{
    public override IActionResult HandleRequest()
    {
        // Check if today is April 1st
        if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
        {
            // Return a 418 I'm a teapot response with an empty body
            return new StatusCodeResult(418);
        }

        // Pass the request to the next handler
        return Successor?.HandleRequest() ?? new NotFoundResult();
    }
}
