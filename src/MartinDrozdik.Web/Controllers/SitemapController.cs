using System;
using Microsoft.AspNetCore.Mvc;
using Bonsai.Sitemap;
using Bonsai.Services.Sitemap;
using Bonsai.Services.Sitemap.Abstraction;
using Microsoft.Extensions.Caching.Memory;
using MartinDrozdik.Web.Configuration;
using Bonsai.Services.LanguageDictionary.Abstraction;
using MartinDrozdik.Web.Views.Sitemap;
using Microsoft.AspNetCore.Mvc.Routing;
using MartinDrozdik.Web.Facades.Models.Blog;
using System.Threading.Tasks;
using MartinDrozdik.Web.Facades.Models.Projects;

namespace Bonsai.Server.Controllers
{
    public class SitemapController : Controller
    {
        const string SITEMAP_NODES_CACHE_KEY = "SitemapNodesCacheKey";

        readonly ServerConfiguration serverConfiguration;
        readonly ICultureProvider cultureProvider;
        readonly ILanguageDictionary languageDictionary;
        readonly IMemoryCache cache;
        private readonly ArticleFacade articleFacade;
        private readonly ProjectFacade projectFacade;

        public SitemapController(ServerConfiguration serverConfiguration,
            ICultureProvider cultureProvider,
            ILanguageDictionary languageDictionary,
            IMemoryCache cache,
            ArticleFacade articleFacade,
            ProjectFacade projectFacade)
        {
            this.serverConfiguration = serverConfiguration;
            this.cultureProvider = cultureProvider;
            this.languageDictionary = languageDictionary;
            this.cache = cache;
            this.articleFacade = articleFacade;
            this.projectFacade = projectFacade;
        }

        /// <summary>
        /// Creates a sitemap of this web from scratch
        /// </summary>
        /// <returns></returns>
        protected async Task<ISitemapNode> CreateSitemapNodeAsync()
        {
            //Home
            SitemapNode home = new SitemapNode(serverConfiguration.Web.Name, new Uri(serverConfiguration.Domain), SitemapChangeFrequency.Weekly, 1);

            //Projects
            SitemapNode projects = new SitemapNode("Projekty", new Uri($"{serverConfiguration.Domain}/projects"), SitemapChangeFrequency.Weekly, 0.7);
            var projectsList = await projectFacade.GetVisibleAsync();
            var newestProjectChange = DateTime.MinValue;
            foreach (var project in projectsList)
            {
                //Save newest edit
                if (project.LastEditAt > newestProjectChange)
                    newestProjectChange = project.LastEditAt;

                //Add article node
                projects.AddChild(new SitemapNode(project.Name, new Uri($"{serverConfiguration.Domain}/project/{project.UrlName}"), SitemapChangeFrequency.Monthly, lastModification: project.LastEditAt));
            }

            //Set last modificatin for projects
            if (newestProjectChange != DateTime.MinValue)
                projects.LastModification = newestProjectChange;

            //Blog
            SitemapNode blog = new SitemapNode("Blog", new Uri($"{serverConfiguration.Domain}/blog"), SitemapChangeFrequency.Weekly, 0.7);
            var articles = await articleFacade.GetPublishedAsync();
            var newestBlogChange = DateTime.MinValue;
            foreach(var article in articles)
            {
                //Save newest edit
                if (article.LastEditAt > newestBlogChange)
                    newestBlogChange = article.LastEditAt;

                //Add article node
                blog.AddChild(new SitemapNode(article.Name, new Uri($"{serverConfiguration.Domain}/blog/{article.UrlName}"), SitemapChangeFrequency.Monthly, lastModification: article.LastEditAt));
            }
            
            //Set last modificatin for blog
            if (newestBlogChange != DateTime.MinValue)
                blog.LastModification = newestBlogChange;


            home.AddChild(projects);
            home.AddChild(blog);
            return home;
        }

        /// <summary>
        /// Returns sitemap root node of this web.
        /// Handles caching etc.
        /// </summary>
        /// <returns></returns>
        protected async Task<ISitemapNode> GetSitemapRootNodeAsync()
        {
            //Cache not found
            if (!cache.TryGetValue(SITEMAP_NODES_CACHE_KEY, out var cacheEntry))
            {
                cacheEntry = await CreateSitemapNodeAsync();

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
        public async Task<IActionResult> Index()
        {
            return View(new SitemapPageModel(serverConfiguration, cultureProvider, languageDictionary, await GetSitemapRootNodeAsync()));
        }

        [Route("/sitemap.xml")]
        public async Task<IActionResult> SitemapFile()
        {
            var rootNode = await GetSitemapRootNodeAsync();
            var xml = rootNode.GenerateXML();

            return Content(xml.ToString(), "text/xml");
        }
    }
}
