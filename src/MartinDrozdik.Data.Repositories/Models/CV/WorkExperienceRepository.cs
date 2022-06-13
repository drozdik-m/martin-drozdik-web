using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.DbContexts;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Data.Repositories.Traits;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.CV
{
    public class WorkExperienceRepository : CRUDRepository<WorkExperience, int, AppDb>,
        IOrderableRepositoryTrait<WorkExperience, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<WorkExperience, int, AppDb> orderableTrait;

        public WorkExperienceRepository(AppDb context) : base(context)
        {
            orderableTrait = this;
        }

        protected override DbSet<WorkExperience> EntitySet => Context.WorkExperiences;

        protected override Expression<Func<WorkExperience, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        protected override async Task<WorkExperience> ProcessNewEntityAsync(WorkExperience entity)
        {
            entity = await base.ProcessNewEntityAsync(entity);
            entity = await orderableTrait.SetInitialOrderAsync(entity);
            return entity;
        }


        protected override async Task<IQueryable<WorkExperience>> ProcessReturnedEntitiesAsync(IQueryable<WorkExperience> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            return orderableTrait.Order(entities);
        }

        protected override async Task<WorkExperience> ProcessDeletedEntityAsync(WorkExperience entity)
        {
            entity = await base.ProcessDeletedEntityAsync(entity);
            return entity;
        }

        #region Orderable trait

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
