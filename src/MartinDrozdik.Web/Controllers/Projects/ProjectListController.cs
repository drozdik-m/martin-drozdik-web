using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Services;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Projects.Display;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.Projects;
using MartinDrozdik.Web.Services.ViewRenderer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Controllers.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectListController : Controller
    {
        private readonly ProjectFacade facade;
        private readonly IViewRenderService viewRenderService;

        public ProjectListController(
            ProjectFacade facade,
            IViewRenderService viewRenderService)
        {
            this.facade = facade;
            this.viewRenderService = viewRenderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectListItem>>> GetAsync()
        {
            try
            {
                var projects = await facade.GetVisibleAsync();
                var result = new List<ProjectListItem>();
                foreach(var item in projects)
                {
                    result.Add(new ProjectListItem()
                    {
                        Id = item.Id,
                        Tags = item.Tags.Select(e => e.Id).ToList(),
                        HTML = await viewRenderService.RenderToStringAsync("Project/ProjectListItem", item),
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
