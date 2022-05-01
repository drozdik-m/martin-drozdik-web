using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Data.Repositories.Models.Projects;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;

namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class ProjectFacade : CRUDFacade<Project, int>,
        IOrderableFacadeTrait<Project, int>,
        IHideableFacadeTrait<Project, int>
    {
        readonly IOrderableFacadeTrait<Project, int> orderableTrait;
        readonly IHideableFacadeTrait<Project, int> hideableTrait;

        readonly ProjectRepository repository;
        readonly ProjectLogoFacade logoFacade;
        readonly ProjectOgImageFacade ogImageFacade;
        readonly ProjectPreviewImageFacade previewImageFacade;
        readonly ProjectGalleryImageFacade galleryImageFacade;

        public ProjectFacade(ProjectRepository repository,
            ProjectLogoFacade logoFacade,
            ProjectOgImageFacade ogImageFacade,
            ProjectPreviewImageFacade previewImageFacade,
            ProjectGalleryImageFacade galleryImageFacade) : base(repository)
        {
            orderableTrait = this;
            hideableTrait = this;
            this.repository = repository;
            this.logoFacade = logoFacade;
            this.ogImageFacade = ogImageFacade;
            this.previewImageFacade = previewImageFacade;
            this.galleryImageFacade = galleryImageFacade;
        }


        public override async Task DeleteAsync(int id)
        {
            var item = await repository.GetAsync(id);

            await logoFacade.DeleteMediaAsync(item.Logo);
            logoFacade.DisposeMediaFolder(item.Logo);

            await ogImageFacade.DeleteMediaAsync(item.OgImage);
            ogImageFacade.DisposeMediaFolder(item.OgImage);

            await previewImageFacade.DeleteMediaAsync(item.PreviewImage);
            previewImageFacade.DisposeMediaFolder(item.PreviewImage);

            foreach(var galleryImage in item.GalleryImages)
            {
                await galleryImageFacade.DeleteMediaAsync(galleryImage);
                galleryImageFacade.DisposeMediaFolder(galleryImage);
            }

            await base.DeleteAsync(id);
        }

        public override async Task UpdateAsync(int id, Project item)
        {
            var oldItem = await repository.ReadAsync(id);

            var deletedGalleryImages = oldItem.GalleryImages
                .Where(e => e.Id != default)
                .Where(e => item.GalleryImages.All(f => e.Id != f.Id));

            foreach (var galleryImage in deletedGalleryImages)
            {
                galleryImageFacade.DisposeMediaFile(galleryImage);
                galleryImageFacade.DisposeMediaFolder(galleryImage);
            }

            await base.UpdateAsync(id, item);
        }

        #region Orderable trait
        IOrderableRepository<Project, int> IOrderableFacadeTrait<Project, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion

        #region Hideable trait
        IHideableRepository<Project, int> IHideableFacadeTrait<Project, int>.HideableRepository => repository;
        public Task<IEnumerable<Project>> GetVisibleAsync() => hideableTrait.TGetVisibleAsync();
        public Task HideAsync(int itemToHide) => hideableTrait.THideAsync(itemToHide);
        public Task ShowAsync(int itemToShow) => hideableTrait.TShowAsync(itemToShow);
        public Task ToggleVisibilityAsync(int itemToToggle) => hideableTrait.TToggleVisibilityAsync(itemToToggle);
        #endregion
    }
}
