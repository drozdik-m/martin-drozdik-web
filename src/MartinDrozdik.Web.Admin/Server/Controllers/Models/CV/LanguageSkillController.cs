using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Server.Controllers.BaseControllers;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.CV
{
    public class LanguageSkillController : BaseApiController<LanguageSkill, int>,
        IOrderableControllerTrait<LanguageSkill, int>,
        ISeedableControllerTrait
    {
        readonly LanguageSkillFacade facade;

        readonly IOrderableControllerTrait<LanguageSkill, int> orderableTrait;
        readonly ISeedableControllerTrait seedableTrait;

        public LanguageSkillController(LanguageSkillFacade facade)
            : base(facade)
        {
            this.facade = facade;

            orderableTrait = this;
            seedableTrait = this;
        }

        #region Orderable trait

        IOrderableFacade<LanguageSkill, int> IOrderableControllerTrait<LanguageSkill, int>.OrderableFacade => facade;

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
