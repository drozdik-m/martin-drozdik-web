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
    public class EducationController : BaseApiController<Education, int>,
        IOrderableControllerTrait<Education, int>,
        ISeedableControllerTrait
    {
        readonly EducationFacade facade;

        readonly IOrderableControllerTrait<Education, int> orderableTrait;
        readonly ISeedableControllerTrait seedableTrait;

        public EducationController(EducationFacade facade)
            : base(facade)
        {
            this.facade = facade;

            orderableTrait = this;
            seedableTrait = this;
        }

        #region Orderable trait

        IOrderableFacade<Education, int> IOrderableControllerTrait<Education, int>.OrderableFacade => facade;

        [HttpPut("reorder")]
        public virtual Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

        #region Seedable trait

        ISeedableFacade ISeedableControllerTrait.SeedableFacade => facade;

        [HttpPost("seed")]
        public virtual Task SeedAsync() => seedableTrait.TSeedAsync();

        #endregion

    }
}
