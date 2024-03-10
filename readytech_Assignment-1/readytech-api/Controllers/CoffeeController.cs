using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;

namespace CoffeeNameSpace
{

    [ApiController]
    [Route("[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly IRequestHandler _handlerChain;

        public CoffeeController(IRequestHandler handlerChain)
        {
            _handlerChain = handlerChain;
        }

        [HttpGet("brew-coffee")]
        public IActionResult BrewCoffee()
        {
            return _handlerChain.HandleRequest();
        }
    }
}