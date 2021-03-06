using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Server.Controllers.BaseControllers;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.Blog;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.Blog
{
    public class ArticleController : BaseApiController<Article, int>,
        IHideableControllerTrait<Article, int>
    {
        readonly ArticleFacade facade;

        readonly IHideableControllerTrait<Article, int> hideableTrait;

        public ArticleController(ArticleFacade facade)
            : base(facade)
        {
            this.facade = facade;

            hideableTrait = this;
        }

        #region Hideable trait
        IHideableFacade<Article, int> IHideableControllerTrait<Article, int>.HideableFacade => facade;

        [HttpPut("{id}/visibility")]
        public virtual Task HideAsync(int id, [FromBody] bool newVisibility) => hideableTrait.TUpdateVisibilityAsync(id, newVisibility);
        #endregion
    }
}
