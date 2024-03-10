// FifthCallHandler.cs
using Microsoft.AspNetCore.Mvc;

public class FifthCallHandler : BaseHandler
{
    private readonly ICallCountService _callCountService;

    public FifthCallHandler(ICallCountService callCountService)
    {
        _callCountService = callCountService;
    }

    public override IActionResult HandleRequest()
    {
        // Check if it's the fifth call
        if (_callCountService.ShouldReturn503())
        {
            // Return a 503 Service Unavailable response with an empty body
            return new StatusCodeResult(503);
        }

        // Pass the request to the next handler
        return Successor?.HandleRequest() ?? new NotFoundResult();
    }
}
