using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Server.Controllers.BaseControllers;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.Projects
{
    public class ProjectController : BaseApiController<Project, int>,
        IOrderableControllerTrait<Project, int>,
        IHideableControllerTrait<Project, int>
    {
        readonly ProjectFacade facade;

        readonly IOrderableControllerTrait<Project, int> orderableTrait;
        readonly IHideableControllerTrait<Project, int> hideableTrait;

        public ProjectController(ProjectFacade facade)
            : base(facade)
        {
            this.facade = facade;

            orderableTrait = this;
            hideableTrait = this;
        }

        #region Orderable trait
        IOrderableFacade<Project, int> IOrderableControllerTrait<Project, int>.OrderableFacade => facade;

        [HttpPut("reorder")]
        public virtual Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion

        #region Hideable trait
        IHideableFacade<Project, int> IHideableControllerTrait<Project, int>.HideableFacade => facade;

        [HttpPut("{id}/visibility")]
        public virtual Task HideAsync(int id, [FromBody] bool newVisibility) => hideableTrait.TUpdateVisibilityAsync(id, newVisibility);
        #endregion
    }
}
