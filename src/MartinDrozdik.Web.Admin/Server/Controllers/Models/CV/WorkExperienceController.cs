using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.CV
{
    public class WorkExperienceController : BaseApiController<WorkExperience, int>,
        IOrderableControllerTrait<WorkExperience, int>
    {
        readonly WorkExperienceFacade facade;

        readonly IOrderableControllerTrait<WorkExperience, int> orderableTrait;

        public WorkExperienceController(WorkExperienceFacade facade)
            : base(facade)
        {
            this.facade = facade;

            orderableTrait = this;
        }

        #region Orderable trait

        IOrderableFacade<WorkExperience, int> IOrderableControllerTrait<WorkExperience, int>.OrderableFacade => facade;

        [HttpPut("reorder")]
        public virtual Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion


    }
}
