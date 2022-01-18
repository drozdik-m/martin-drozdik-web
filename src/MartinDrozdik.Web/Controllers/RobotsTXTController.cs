using System;
using Microsoft.AspNetCore.Mvc;
using Bonsai.Services.RobotsTXT;
using MartinDrozdik.Web.Configuration;

namespace Bonsai.Server.Controllers
{
    public class RobotsTXTController : Controller
    {
        readonly ServerConfiguration serverConfiguration;

        public RobotsTXTController(ServerConfiguration serverConfiguration)
        {
            this.serverConfiguration = serverConfiguration;
        }

        /// <summary>
        /// Creates contents of the robots.txt file.
        /// </summary>
        /// <returns></returns>
        string CreateRobotsTXTContent()
        {
            var builder = new RobotsTXTBuilder();

            //Add sitemap
            var sitemapUri = new Uri(serverConfiguration.Web.Domain + "/sitemap.xml");
            builder.AddSitemap(sitemapUri);

            //Allow all
            builder.Allow("/", UserAgent.Everyone);

            return builder.Content();
        }

        /// <summary>
        /// Returns contents of the robots.txt file.
        /// Can handle caching etc.
        /// </summary>
        /// <returns></returns>
        string GetRobotsTXTContent() => CreateRobotsTXTContent();


        [Route("/robots.txt")]
        public IActionResult SitemapFile()
        {
            return Content(GetRobotsTXTContent(), "text/plain");
        }
    }
}
