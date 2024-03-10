// BaseHandler.cs
using Microsoft.AspNetCore.Mvc;

public abstract class BaseHandler : IRequestHandler
{
    protected IRequestHandler Successor { get; private set; }

    public IRequestHandler SetSuccessor(IRequestHandler successor)
    {
        Successor = successor;
        return successor;
    }

    public abstract IActionResult HandleRequest();
}
