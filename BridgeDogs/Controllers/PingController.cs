using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BridgeDogs.Controllers
{
    [Route("ping")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private const string VERSION = "Dogshouseservice.Version1.0.1";

        [HttpGet]
        public string Ping()
        {
            // TODO: I guess we can use `Assembly` and Reflection here to avoid hardcoded string.
            return VERSION;
        }
    }
}
