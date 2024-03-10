// DefaultHandler.cs
using System;
using Microsoft.AspNetCore.Mvc;

public class DefaultHandler : BaseHandler
{
    public override IActionResult HandleRequest()
    {
        // Get the current date/time in ISO-8601 format
        string formattedDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");

        // Prepare the response JSON object
        var response = new
        {
            message = "Your piping hot coffee is ready",
            prepared = formattedDateTime
        };

        // Return a 200 OK response with the JSON object
        return new OkObjectResult(response);
    }
}
