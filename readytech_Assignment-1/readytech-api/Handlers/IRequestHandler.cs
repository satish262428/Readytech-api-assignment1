// IRequestHandler.cs
using Microsoft.AspNetCore.Mvc;

public interface IRequestHandler
{
    IActionResult HandleRequest();
    IRequestHandler SetSuccessor(IRequestHandler successor);
}
