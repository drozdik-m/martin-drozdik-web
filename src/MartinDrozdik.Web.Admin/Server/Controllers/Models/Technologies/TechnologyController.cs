using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Technologies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.Technologies
{
    public class TechnologyController : BaseApiController<Technology, int>,
        IOrderableControllerTrait<Technology, int>
    {
        readonly TechnologyFacade facade;

        readonly IOrderableControllerTrait<Technology, int> orderableTrait;

        public TechnologyController(TechnologyFacade facade)
            : base(facade)
        {
            this.facade = facade;

            orderableTrait = this;
        }

        #region Orderable trait

        IOrderableFacade<Technology, int> IOrderableControllerTrait<Technology, int>.OrderableFacade => facade;

        [HttpPut("reorder")]
        public virtual Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion


    }
}
