using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Services;
using MartinDrozdik.Data.DbContexts.Seeds.CVSeed;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.CV;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Web.Facades.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;

namespace MartinDrozdik.Web.Facades.Models.People
{
    public class EducationFacade : CRUDFacade<Education, int>,
        IOrderableFacadeTrait<Education, int>,
        ISeedableFacadeTrait
    {
        readonly IOrderableFacadeTrait<Education, int> orderableTrait;
        readonly ISeedableFacadeTrait seedableTrait;

        private readonly EducationRepository repository;
        private readonly EducationSeed seed;

        public EducationFacade(EducationRepository repository,
            EducationSeed seed) : base(repository)
        {
            orderableTrait = this;
            seedableTrait = this;
            this.repository = repository;
            this.seed = seed;
        }

        #region Orderable trait

        IOrderableRepository<Education, int> IOrderableFacadeTrait<Education, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

        #region Seedable trait

        ISeedableService ISeedableFacadeTrait.SeedableService => seed;

        public Task SeedAsync() => seedableTrait.TSeedAsync();

        #endregion
    }
}
