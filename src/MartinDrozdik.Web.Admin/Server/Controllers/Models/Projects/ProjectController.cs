using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.Projects
{
    public class ProjectController : BaseApiController<Project, int>,
        IOrderableControllerTrait<Project, int>
    {
        readonly ProjectFacade facade;

        readonly IOrderableControllerTrait<Project, int> orderableTrait;

        public ProjectController(ProjectFacade facade)
            : base(facade)
        {
            this.facade = facade;

            orderableTrait = this;
        }

        #region Orderable trait

        IOrderableFacade<Project, int> IOrderableControllerTrait<Project, int>.OrderableFacade => facade;

        [HttpPut("reorder")]
        public virtual Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion


    }
}
