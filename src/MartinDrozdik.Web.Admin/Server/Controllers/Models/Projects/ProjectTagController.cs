using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bonsai.Server.Controllers.Localization
{
    public class ProjectTagController : BaseApiController<ProjectTag, int>,
        IOrderableControllerTrait<ProjectTag, int>
    {
        readonly ProjectTagFacade facade;

        readonly IOrderableControllerTrait<ProjectTag, int> orderableTrait;

        public ProjectTagController(ProjectTagFacade facade)
            :base(facade)
        {
            this.facade = facade;

            orderableTrait = this;
        }

        #region Orderable trait

        IOrderableFacade<ProjectTag, int> IOrderableControllerTrait<ProjectTag, int>.OrderableFacade => facade;

        [HttpPost("reorder")]
        public virtual Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.ReorderAsync(newOrder);

        #endregion


    }
}
