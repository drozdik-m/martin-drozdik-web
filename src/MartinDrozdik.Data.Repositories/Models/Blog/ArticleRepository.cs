using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;
using MartinDrozdik.Data.DbContexts;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Data.Repositories.Traits;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.Blog
{
    public class ArticleRepository : CRUDRepository<Article, int, AppDb>,
        IOrderableRepositoryTrait<Article, int, AppDb>,
        IHideableRepositoryTrait<Article, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<Article, int, AppDb> orderableTrait;
        readonly IHideableRepositoryTrait<Article, int, AppDb> hideableTrait;

        public ArticleRepository(AppDb context) : base(context)
        {
            orderableTrait = this;
            hideableTrait = this;
        }

        protected override DbSet<Article> EntitySet => Context.Articles;

        protected override Expression<Func<Article, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        public async Task<Article> GetAsync(string id)
        {
            var entities = EntitySet
                .Where(e => e.UrlName == id);
            var includedEntities = await IncludeRelationsAsync(entities);
            var processedEntities = await ProcessReturnedEntitiesAsync(includedEntities);
            var searchedEntity = await processedEntities.FirstOrDefaultAsync();

            //Not found
            if (searchedEntity == default)
                throw new NotFoundException();

            //Return result entity
            return searchedEntity;
        }

        protected override Task<IQueryable<Article>> IncludeRelationsAsync(IQueryable<Article> entities)
        {
            entities = entities
                .Include(e => e.MainImage)
                .Include(e => e.Content)
                .Include(e => e.Author)
                    .ThenInclude(e => e.ProfileImage)
                .Include(e => e.Tags.OrderBy(e => e.Tag.OrderIndex))
                    .ThenInclude(e => e.Tag)
                ;
            return base.IncludeRelationsAsync(entities);
        }

        protected override async Task<Article> ProcessNewEntityAsync(Article entity)
        {
            entity = await base.ProcessNewEntityAsync(entity);
            Context.Entry(entity.Content).State = EntityState.Added;
            Context.Entry(entity.MainImage).State = EntityState.Added;
            entity = await orderableTrait.SetInitialOrderAsync(entity);
            return entity;
        }

        protected override async Task<Article> ProcessUpdatedEntityAsync(Article entity)
        {
            entity = await base.ProcessUpdatedEntityAsync(entity);
            Context.Entry(entity.Content).State = EntityState.Modified;
            Context.Entry(entity.MainImage).State = EntityState.Modified;
            entity.AuthorId = entity.Author?.Id;

            //await HandleManyToManyUpdate<ProjectTag, int>(entity, e => e.Tags, e => e.Tags);
            await HandleManyToManyConnectorUpdate<ProjectHasTag, int>(entity, e => e.Tags, e => e.Tags);

            return entity;
        }

        protected override async Task<IQueryable<Article>> ProcessReturnedEntitiesAsync(IQueryable<Article> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            entities = orderableTrait.Order(entities);
            return entities;
        }

        protected override async Task<Article> ProcessDeletedEntityAsync(Article entity)
        {
            entity = await base.ProcessDeletedEntityAsync(entity);
            Context.Entry(entity.Content).State = EntityState.Deleted;
            Context.Entry(entity.MainImage).State = EntityState.Deleted;
            return entity;
        }

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion

        #region Hideable trait
        public Task<IEnumerable<Article>> GetVisibleAsync() => hideableTrait.TGetVisibleAsync();
        public Task HideAsync(int itemToHide) => hideableTrait.THideAsync(itemToHide);
        public Task ShowAsync(int itemToShow) => hideableTrait.TShowAsync(itemToShow);
        public Task ToggleVisibilityAsync(int itemToToggle) => hideableTrait.TToggleVisibilityAsync(itemToToggle);
        #endregion
    }
}
