using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Services;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Blog.Display;
using MartinDrozdik.Data.Models.Projects.Display;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.Blog;
using MartinDrozdik.Web.Facades.Models.Projects;
using MartinDrozdik.Web.Services.ViewRenderer;
using MartinDrozdik.Web.Views.Blog;
using MartinDrozdik.Web.Views.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Controllers.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleListController : Controller
    {
        private readonly ArticleFacade facade;
        private readonly IViewRenderService viewRenderService;

        public ArticleListController(
            ArticleFacade facade,
            IViewRenderService viewRenderService)
        {
            this.facade = facade;
            this.viewRenderService = viewRenderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleListItem>>> GetAsync()
        {
          
            try
            {
                //Get projects
                var projects = await facade.GetPublishedAsync();
                var result = new List<ArticleListItem>();
                foreach(var item in projects)
                {
                    var model = new ArticleListItemModel(item);
                    result.Add(new ArticleListItem()
                    {
                        Id = item.Id,
                        Tags = item.Tags.Select(e => e.Tag.Id).ToList(),
                        HTML = await viewRenderService.RenderToStringAsync("Blog/_ArticleListItem", model),
                    });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
