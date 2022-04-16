using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Data.Repositories.Traits;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.Technologies
{
    public class TechnologyRepository : CRUDRepository<Technology, int, AppDb>,
        IOrderableRepositoryTrait<Technology, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<Technology, int, AppDb> orderableTrait;

        public TechnologyRepository(AppDb context) : base(context)
        {
            orderableTrait = this;
        }

        protected override DbSet<Technology> EntitySet => Context.Technologies;

        protected override Expression<Func<Technology, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        protected override Task<IQueryable<Technology>> IncludeRelationsAsync(IQueryable<Technology> entities)
        {
            entities = entities.Include(e => e.Logo);
            return base.IncludeRelationsAsync(entities);
        }

        protected override async Task<Technology> ProcessNewEntityAsync(Technology entity)
        {
            entity = await base.ProcessNewEntityAsync(entity);
            Context.Entry(entity.Logo).State = EntityState.Added;
            entity = await orderableTrait.SetInitialOrderAsync(entity);
            return entity;
        }

        protected override async Task<Technology> ProcessUpdatedEntityAsync(Technology entity)
        {
            entity = await base.ProcessUpdatedEntityAsync(entity);
            Context.Entry(entity.Logo).State = EntityState.Modified;
            return entity;
        }

        protected override async Task<IQueryable<Technology>> ProcessReturnedEntitiesAsync(IQueryable<Technology> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            return orderableTrait.Order(entities);
        }

        protected override async Task<Technology> ProcessDeletedEntityAsync(Technology entity)
        {
            entity = await base.ProcessDeletedEntityAsync(entity);
            Context.Entry(entity.Logo).State = EntityState.Deleted;

            return entity;
        }

        #region Orderable trait

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
