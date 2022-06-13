using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Server.Controllers.Models.Media.Images;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.Projects
{
    public class ProjectGalleryImageController : ImageController<ProjectGalleryImage>,
        IOrderableControllerTrait<ProjectGalleryImage, int>
    {
        readonly ProjectGalleryImageFacade facade;

        readonly IOrderableControllerTrait<ProjectGalleryImage, int> orderableTrait;

        public ProjectGalleryImageController(ProjectGalleryImageFacade facade)
            : base(facade)
        {
            this.facade = facade;
            orderableTrait = this;
        }

        #region Orderable trait
        IOrderableFacade<ProjectGalleryImage, int> IOrderableControllerTrait<ProjectGalleryImage, int>.OrderableFacade => facade;

        [HttpPut("reorder")]
        public virtual Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion
    }
}
