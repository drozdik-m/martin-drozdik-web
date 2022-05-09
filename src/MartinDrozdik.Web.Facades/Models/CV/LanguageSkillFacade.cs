using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Services;
using MartinDrozdik.Data.DbContexts.Seeds.CVSeeds;
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
    public class LanguageSkillFacade : CRUDFacade<LanguageSkill, int>,
        IOrderableFacadeTrait<LanguageSkill, int>,
        ISeedableFacadeTrait
    {
        readonly IOrderableFacadeTrait<LanguageSkill, int> orderableTrait;
        readonly ISeedableFacadeTrait seedableTrait;

        private readonly LanguageSkillRepository repository;
        private readonly LanguageSkillSeed seed;

        public LanguageSkillFacade(LanguageSkillRepository repository,
            LanguageSkillSeed seed) : base(repository)
        {
            orderableTrait = this;
            seedableTrait = this;
            this.repository = repository;
            this.seed = seed;
        }

        #region Orderable trait

        IOrderableRepository<LanguageSkill, int> IOrderableFacadeTrait<LanguageSkill, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

        #region Seedable trait

        ISeedableService ISeedableFacadeTrait.SeedableService => seed;

        public Task SeedAsync() => seedableTrait.TSeedAsync();

        #endregion
    }
}
