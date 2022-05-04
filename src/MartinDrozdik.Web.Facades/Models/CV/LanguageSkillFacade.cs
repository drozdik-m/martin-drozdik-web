using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        IOrderableFacadeTrait<LanguageSkill, int>
    {
        readonly IOrderableFacadeTrait<LanguageSkill, int> orderableTrait;

        private readonly LanguageSkillRepository repository;

        public LanguageSkillFacade(LanguageSkillRepository repository) : base(repository)
        {
            orderableTrait = this;
            this.repository = repository;
        }

        #region Orderable trait

        IOrderableRepository<LanguageSkill, int> IOrderableFacadeTrait<LanguageSkill, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
