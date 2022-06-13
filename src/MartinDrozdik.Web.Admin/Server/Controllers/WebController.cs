using MartinDrozdik.Web.Admin.Server.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers
{
    [Route("web")]
    public class WebController : Controller
    {
        private readonly ServerConfiguration config;

        public WebController(ServerConfiguration config)
        {
            this.config = config;
        }

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            return Redirect(config.WebDomain);
        }
    }
}
