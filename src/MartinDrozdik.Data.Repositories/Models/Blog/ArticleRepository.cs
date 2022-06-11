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
        IHideableRepositoryTrait<Article, int, AppDb>
    {
        readonly IHideableRepositoryTrait<Article, int, AppDb> hideableTrait;

        public ArticleRepository(AppDb context) : base(context)
        {
            hideableTrait = this;
        }

        protected override DbSet<Article> EntitySet => Context.Articles;

        protected override Expression<Func<Article, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        /// <summary>
        /// Returns article based on Url Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
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

        protected Expression<Func<Article, bool>> IsPublished { get; set; }
            = e => (!e.PublishDate.HasValue || e.PublishDate <= DateTime.Now) && !e.IsHidden;

        /// <summary>
        /// Return all articles that have been published
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> GetPublishedAsync()
        {
            var allEntities = await IncludeRelationsAsync(EntitySet.AsNoTracking());
            var published = allEntities
                .Where(IsPublished);
            var processedEntities = await ProcessReturnedEntitiesAsync(published);
            return await processedEntities.ToListAsync();
        }

        /// <summary>
        /// Return first n articles that have been published
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> GetFirstPublishedAsync(int count)
        {
            var allEntities = await IncludeRelationsAsync(EntitySet.AsNoTracking());
            var published = allEntities
                .Where(IsPublished)
                .Take(count);
            var processedEntities = await ProcessReturnedEntitiesAsync(published);
            return await processedEntities.ToListAsync();
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
            return entity;
        }

        protected override async Task<Article> ProcessUpdatedEntityAsync(Article entity)
        {
            entity = await base.ProcessUpdatedEntityAsync(entity);
            Context.Entry(entity.Content).State = EntityState.Modified;
            Context.Entry(entity.MainImage).State = EntityState.Modified;
            entity.AuthorId = entity.Author?.Id;

            await HandleManyToManyConnectorUpdate<ArticleHasTag, int>(entity, e => e.Tags, e => e.Tags);

            return entity;
        }

        protected override async Task<IQueryable<Article>> ProcessReturnedEntitiesAsync(IQueryable<Article> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            entities = entities.OrderByDescending(e => e.PublishDate);
            return entities;
        }

        protected override async Task<Article> ProcessDeletedEntityAsync(Article entity)
        {
            entity = await base.ProcessDeletedEntityAsync(entity);
            Context.Entry(entity.Content).State = EntityState.Deleted;
            Context.Entry(entity.MainImage).State = EntityState.Deleted;
            return entity;
        }

        #region Hideable trait
        public Task<IEnumerable<Article>> GetVisibleAsync() => hideableTrait.TGetVisibleAsync();
        public Task HideAsync(int itemToHide) => hideableTrait.THideAsync(itemToHide);
        public Task ShowAsync(int itemToShow) => hideableTrait.TShowAsync(itemToShow);
        public Task ToggleVisibilityAsync(int itemToToggle) => hideableTrait.TToggleVisibilityAsync(itemToToggle);
        #endregion
    }
}
