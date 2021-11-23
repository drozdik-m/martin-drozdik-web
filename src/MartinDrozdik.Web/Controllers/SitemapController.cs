using System;
using Microsoft.AspNetCore.Mvc;
using Bonsai.Sitemap;
using Bonsai.Services.Sitemap;
using Bonsai.Services.Sitemap.Abstraction;
using Microsoft.Extensions.Caching.Memory;
using MartinDrozdik.Web.Configuration;

namespace Bonsai.Server.Controllers
{
    public class SitemapController : Controller
    {
        const string SITEMAP_NODES_CACHE_KEY = "SitemapNodesCacheKey";

        readonly WebConfiguration webConfiguration;
        readonly IMemoryCache cache;

        public SitemapController(WebConfiguration webConfiguration,
             IMemoryCache cache)
        {
            this.webConfiguration = webConfiguration;
            this.cache = cache;
        }

        /// <summary>
        /// Creates a sitemap of this web from scratch
        /// </summary>
        /// <returns></returns>
        protected ISitemapNode CreateSitemapNode()
        {
            SitemapNode home = new SitemapNode(webConfiguration.WebName, new Uri(webConfiguration.Domain), SitemapChangeFrequency.Monthly, 1);

            return home;
        }

        /// <summary>
        /// Returns sitemap root node of this web.
        /// Handles caching etc.
        /// </summary>
        /// <returns></returns>
        protected ISitemapNode GetSitemapRootNode()
        {
            //Cache not found
            if (!cache.TryGetValue(SITEMAP_NODES_CACHE_KEY, out var cacheEntry))
            {
                cacheEntry = CreateSitemapNode();

                //Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(8))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(24));

                //Save data in cache
                cache.Set(SITEMAP_NODES_CACHE_KEY, cacheEntry, cacheEntryOptions);
            }


            return cacheEntry as ISitemapNode;
        }

        [HttpPost("/sitemap/update")]
        public IActionResult UpdateSitemap()
        {
            cache.Remove(SITEMAP_NODES_CACHE_KEY);

            return Ok();
        }

        [Route("/sitemap")]
        public IActionResult Index()
        {
            return NotFound();
            //return View(new SitemapPageModel(languageDictionary, GetSitemapRootNode()));
        }

        [Route("/sitemap.xml")]
        public IActionResult SitemapFile()
        {
            var rootNode = GetSitemapRootNode();
            var xml = rootNode.GenerateXML();

            return Content(xml.ToString(), "text/xml");
        }
    }
}
