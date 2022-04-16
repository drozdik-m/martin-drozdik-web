using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Web.Facades.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;

namespace MartinDrozdik.Web.Facades.Models.People
{
    public class PersonFacade : CRUDFacade<Person, int>,
        IOrderableFacadeTrait<Person, int>
    {
        readonly IOrderableFacadeTrait<Person, int> orderableTrait;

        private readonly PersonRepository repository;
        private readonly PersonProfileImageFacade profileImageFacade;

        public PersonFacade(PersonRepository repository,
            PersonProfileImageFacade profileImageFacade) : base(repository)
        {
            orderableTrait = this;
            this.repository = repository;
            this.profileImageFacade = profileImageFacade;
        }


        public override async Task DeleteAsync(int id)
        {
            var item = await repository.GetAsync(id);
            await profileImageFacade.DeleteMediaAsync(item.ProfileImage);
            await base.DeleteAsync(id);
        }

        #region Orderable trait

        IOrderableRepository<Person, int> IOrderableFacadeTrait<Person, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
