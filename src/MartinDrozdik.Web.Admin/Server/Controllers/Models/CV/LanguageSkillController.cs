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
    public class LanguageSkillController : BaseApiController<LanguageSkill, int>,
        IOrderableControllerTrait<LanguageSkill, int>
    {
        readonly LanguageSkillFacade facade;

        readonly IOrderableControllerTrait<LanguageSkill, int> orderableTrait;

        public LanguageSkillController(LanguageSkillFacade facade)
            : base(facade)
        {
            this.facade = facade;

            orderableTrait = this;
        }

        #region Orderable trait

        IOrderableFacade<LanguageSkill, int> IOrderableControllerTrait<LanguageSkill, int>.OrderableFacade => facade;

        [HttpPut("reorder")]
        public virtual Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion


    }
}
