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

namespace MartinDrozdik.Data.Repositories.Models.Projects
{
    public class ProjectRepository : CRUDRepository<Project, int, AppDb>,
        IOrderableRepositoryTrait<Project, int, AppDb>,
        IHideableRepositoryTrait<Project, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<Project, int, AppDb> orderableTrait;
        readonly IHideableRepositoryTrait<Project, int, AppDb> hideableTrait;

        public ProjectRepository(AppDb context) : base(context)
        {
            orderableTrait = this;
            hideableTrait = this;
        }

        protected override DbSet<Project> EntitySet => Context.Projects;

        protected override Expression<Func<Project, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        protected override Task<IQueryable<Project>> IncludeRelationsAsync(IQueryable<Project> entities)
        {
            entities = entities
                .Include(e => e.Logo)
                .Include(e => e.OgImage)
                .Include(e => e.Tags.OrderBy(e => e.OrderIndex));
            return base.IncludeRelationsAsync(entities);
        }

        protected override async Task<Project> ProcessNewEntityAsync(Project entity)
        {
            entity = await base.ProcessNewEntityAsync(entity);
            Context.Entry(entity.Logo).State = EntityState.Added;
            Context.Entry(entity.OgImage).State = EntityState.Added;
            entity = await orderableTrait.SetInitialOrderAsync(entity);
            return entity;
        }

        protected override async Task<Project> ProcessUpdatedEntityAsync(Project entity)
        {
            entity = await base.ProcessUpdatedEntityAsync(entity);
            Context.Entry(entity.Logo).State = EntityState.Modified;
            Context.Entry(entity.OgImage).State = EntityState.Modified;

            await HandleManyToManyUpdate(entity, e => e.Tags, e => e.Tags);

            return entity;
        }

        protected override async Task<IQueryable<Project>> ProcessReturnedEntitiesAsync(IQueryable<Project> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            entities = orderableTrait.Order(entities);
            return entities;
        }

        protected override async Task<Project> ProcessDeletedEntityAsync(Project entity)
       {
            entity = await base.ProcessDeletedEntityAsync(entity);
            Context.Entry(entity.Logo).State = EntityState.Deleted;
            Context.Entry(entity.OgImage).State = EntityState.Deleted;
            return entity;
        }

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion

        #region Hideable trait
        public Task<IEnumerable<Project>> GetVisibleAsync() => hideableTrait.TGetVisibleAsync();
        public Task HideAsync(int itemToHide) => hideableTrait.THideAsync(itemToHide);
        public Task ShowAsync(int itemToShow) => hideableTrait.TShowAsync(itemToShow);
        public Task ToggleVisibilityAsync(int itemToToggle) => hideableTrait.TToggleVisibilityAsync(itemToToggle);
        #endregion
    }
}
