using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using Bonsai.Models.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Localization;
using MartinDrozdik.Data.Models.Tags;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Traits;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;

namespace MartinDrozdik.Data.Repositories.Models.Projects
{
    public class ProjectGalleryImageRepository : ProjectImageRepository<ProjectGalleryImage>,
        IOrderableRepositoryTrait<ProjectGalleryImage, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<ProjectGalleryImage, int, AppDb> orderableTrait;

        public ProjectGalleryImageRepository(AppDb context)
            : base(context)
        {
            orderableTrait = this;
        }

        protected override DbSet<ProjectGalleryImage> EntitySet => Context.ProjectGalleryImages;

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion
    }
}
