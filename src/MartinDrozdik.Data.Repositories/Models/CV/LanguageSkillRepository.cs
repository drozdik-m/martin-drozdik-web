using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Data.Repositories.Traits;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.CV
{
    public class LanguageSkillRepository : CRUDRepository<LanguageSkill, int, AppDb>,
        IOrderableRepositoryTrait<LanguageSkill, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<LanguageSkill, int, AppDb> orderableTrait;

        public LanguageSkillRepository(AppDb context) : base(context)
        {
            orderableTrait = this;
        }

        protected override DbSet<LanguageSkill> EntitySet => Context.LanguageSkills;

        protected override Expression<Func<LanguageSkill, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        protected override async Task<LanguageSkill> ProcessNewEntityAsync(LanguageSkill entity)
        {
            entity = await base.ProcessNewEntityAsync(entity);
            entity = await orderableTrait.SetInitialOrderAsync(entity);
            return entity;
        }


        protected override async Task<IQueryable<LanguageSkill>> ProcessReturnedEntitiesAsync(IQueryable<LanguageSkill> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            return orderableTrait.Order(entities);
        }

        protected override async Task<LanguageSkill> ProcessDeletedEntityAsync(LanguageSkill entity)
        {
            entity = await base.ProcessDeletedEntityAsync(entity);
            return entity;
        }

        #region Orderable trait

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
