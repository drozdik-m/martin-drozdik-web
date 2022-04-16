using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Data.Repositories.Traits;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.People
{
    public class PersonRepository : CRUDRepository<Person, int, AppDb>,
        IOrderableRepositoryTrait<Person, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<Person, int, AppDb> orderableTrait;

        public PersonRepository(AppDb context) : base(context)
        {
            orderableTrait = this;
        }

        protected override DbSet<Person> EntitySet => Context.People;

        protected override Expression<Func<Person, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        protected override Task<IQueryable<Person>> IncludeRelationsAsync(IQueryable<Person> entities)
        {
            entities = entities.Include(e => e.ProfileImage);
            return base.IncludeRelationsAsync(entities);
        }

        protected override async Task<Person> ProcessNewEntityAsync(Person entity)
        {
            entity = await base.ProcessNewEntityAsync(entity);
            Context.Entry(entity.ProfileImage).State = EntityState.Added;
            entity = await orderableTrait.SetInitialOrderAsync(entity);
            return entity;
        }

        protected override async Task<Person> ProcessUpdatedEntityAsync(Person entity)
        {
            entity = await base.ProcessUpdatedEntityAsync(entity);
            Context.Entry(entity.ProfileImage).State = EntityState.Modified;
            return entity;
        }

        protected override async Task<IQueryable<Person>> ProcessReturnedEntitiesAsync(IQueryable<Person> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            entities = entities.Include(e => e.ProfileImage);
            return orderableTrait.Order(entities);
        }

        protected override async Task<Person> ProcessDeletedEntityAsync(Person entity)
        {
            entity = await base.ProcessDeletedEntityAsync(entity);
            Context.Entry(entity.ProfileImage).State = EntityState.Deleted;

            return entity;
        }

        #region Orderable trait

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
