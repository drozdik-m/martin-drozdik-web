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
        IOrderableFacadeTrait<Project, int>
    {
        readonly IOrderableFacadeTrait<Project, int> orderableTrait;

        readonly ProjectRepository repository;
        readonly ProjectLogoFacade logoFacade;
        readonly ProjectOgImageFacade ogImageFacade;

        public ProjectFacade(ProjectRepository repository,
            ProjectLogoFacade logoFacade,
            ProjectOgImageFacade ogImageFacade) : base(repository)
        {
            orderableTrait = this;
            this.repository = repository;
            this.logoFacade = logoFacade;
            this.ogImageFacade = ogImageFacade;
        }


        public override async Task DeleteAsync(int id)
        {
            var item = await repository.GetAsync(id);
            await logoFacade.DeleteMediaAsync(item.Logo);
            await ogImageFacade.DeleteMediaAsync(item.OgImage);
            await base.DeleteAsync(id);
        }

        #region Orderable trait

        IOrderableRepository<Project, int> IOrderableFacadeTrait<Project, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
