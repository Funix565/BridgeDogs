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
        public ContentResult Ping()
        {
            return Content(VERSION);
        }
    }
}
