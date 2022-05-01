using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Data.Repositories.Models.Technologies;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;

namespace MartinDrozdik.Web.Facades.Models.Technologies
{
    public class TechnologyFacade : CRUDFacade<Technology, int>,
        IOrderableFacadeTrait<Technology, int>
    {
        readonly IOrderableFacadeTrait<Technology, int> orderableTrait;

        private readonly TechnologyRepository repository;
        private readonly TechnologyLogoFacade profileImageFacade;

        public TechnologyFacade(TechnologyRepository repository,
            TechnologyLogoFacade profileImageFacade) : base(repository)
        {
            orderableTrait = this;
            this.repository = repository;
            this.profileImageFacade = profileImageFacade;
        }


        public override async Task DeleteAsync(int id)
        {
            var item = await repository.GetAsync(id);
            await profileImageFacade.DeleteMediaAsync(item.Logo);
            profileImageFacade.DisposeMediaFolder(item.Logo);
            await base.DeleteAsync(id);
        }

        #region Orderable trait

        IOrderableRepository<Technology, int> IOrderableFacadeTrait<Technology, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
