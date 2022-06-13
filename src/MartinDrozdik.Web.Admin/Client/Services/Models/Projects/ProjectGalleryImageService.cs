using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Utils.JSON;
using MartinDrozdik.Data.Models.Files;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Models.Media;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.Projects
{
    public class ProjectGalleryImageService : MediaBaseService<ProjectGalleryImage>,
        IOrderableServiceTrait<ProjectGalleryImage, int>
    {
        readonly IOrderableServiceTrait<ProjectGalleryImage, int> orderableTrait;

        public ProjectGalleryImageService(HttpClient http)
            : base(http)
        {
            orderableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/ProjectGalleryImage";

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion
    }
}
