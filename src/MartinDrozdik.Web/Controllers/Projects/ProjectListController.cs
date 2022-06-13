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
using MartinDrozdik.Web.Views.Projects;
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
        public async Task<ActionResult<IEnumerable<ProjectListItem>>> GetAsync(string scheme)
        {
           
            //ProjectListItemModel
            try
            {
                //Get scheme
                var schemeType = ProjectListItemScheme.Dark;
                if (scheme == "light")
                    schemeType = ProjectListItemScheme.Light;
                else if (scheme != "dark")
                    throw new Exception("Unknown scheme");

                //Get projects
                var projects = await facade.GetVisibleAsync();
                var result = new List<ProjectListItem>();
                foreach(var item in projects)
                {
                    var model = new ProjectListItemModel(item, schemeType);
                    result.Add(new ProjectListItem()
                    {
                        Id = item.Id,
                        Tags = item.Tags.Select(e => e.Tag.Id).ToList(),
                        HTML = await viewRenderService.RenderToStringAsync("Projects/_ProjectListItem", model),
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
