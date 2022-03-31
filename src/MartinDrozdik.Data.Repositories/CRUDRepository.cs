using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.Repositories.Traits.Abstraction;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Exceptions.Services.CRUD;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using MartinDrozdik.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace MartinDrozdik.Data.Repositories
{
    public abstract class CRUDRepository<TEntity, TKey, TContext>
        : ICRUDRepository<TEntity, TKey>, ICRUDRepositoryTrait<TEntity, TKey, TContext>
        where TEntity : class, IIdentifiable<TKey>
        where TContext : DbContext
    {
        /// <summary>
        /// The currently used context
        /// </summary>
        protected TContext Context { get; set; }

        /// <summary>
        /// Returns the target DbSet of controlled entity
        /// </summary>
        protected abstract DbSet<TEntity> EntitySet { get; }

        public CRUDRepository(TContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Includes important relations in an input DbSet
        /// </summary>
        /// <param name="entities">Input DbSet</param>
        /// <returns>IQueryable object for further queuing</returns>
        protected virtual Task<IQueryable<TEntity>> IncludeRelationsAsync(IQueryable<TEntity> entities)
            => Task.FromResult(entities);

        /// <summary>
        /// Updates existing or new relations in an input entity for the context save
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>Updated entity (which is also the input entity)</returns>
        protected virtual Task<TEntity> UpdateRelationsAsync(TEntity entity)
            => Task.FromResult(entity);

        /// <summary>
        /// Processes newly created entity before context is saved
        /// </summary>
        /// <param name="entity">Entity to process</param>
        /// <returns>Processed input entity</returns>
        protected virtual Task<TEntity> ProcessNewEntityAsync(TEntity entity) 
            => Task.FromResult(entity);

        /// <summary>
        /// Processes entities before they are returned
        /// </summary>
        /// <param name="entity">Entity to process</param>
        /// <returns>Processed input entity</returns>
        protected virtual Task<IQueryable<TEntity>> ProcessReturnedEntitiesAsync(IQueryable<TEntity> entities)
            => Task.FromResult(entities);

        /// <summary>
        /// Processes updated entity before the context is saved
        /// </summary>
        /// <param name="entity">Entity to process</param>
        /// <returns>Processed input entity</returns>
        protected virtual Task<TEntity> ProcessUpdatedEntityAsync(TEntity entity)
            => Task.FromResult(entity);

        /// <summary>
        /// Processes deleted entity (logically or physically) before context is saved
        /// </summary>
        /// <param name="entity">Entity to process</param>
        /// <returns>Processed input entity</returns>
        protected virtual Task<TEntity> ProcessDeletedEntityAsync(TEntity entity)
            => Task.FromResult(entity);

        /// <summary>
        /// Predicate for searching using an id
        /// </summary>
        /// <param name="targetId">Target id</param>
        /// <returns>Id search predicate</returns>
        protected abstract Expression<Func<TEntity, bool>> IdPredicate(TKey targetId);

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            var allEntities = await IncludeRelationsAsync(EntitySet);
            var processedEntities = await ProcessReturnedEntitiesAsync(allEntities);
            return await processedEntities.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetAsync(TKey id)
        {
            if (Equals(id, default(TKey)))
                throw new DefaultKeyException();

            var entities = EntitySet.Where(IdPredicate(id));
            var includedEntities = await IncludeRelationsAsync(entities);
            var processedEntities = await ProcessReturnedEntitiesAsync(includedEntities);
            var searchedEntity = await processedEntities.FirstOrDefaultAsync();

            //Not found
            if (searchedEntity == default)
                throw new NotFoundException();

            //Return result entity
            return searchedEntity;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TKey id, TEntity item)
        {
            if (item == default)
                throw new ArgumentNullException(nameof(item));

            if (id == null)
                throw new ArgumentNullException(nameof(id));

            //The entity is not in the database
            var exists = await ExistsAsync(id);
            if (!exists)
                throw new NotFoundException();

            if (!id.Equals(item.Id))
                throw new DifferentIDsException();

            //Set modified state
            Context.Entry(item).State = EntityState.Modified;

            //Update relations before saving
            item = await UpdateRelationsAsync(item);

            //Process before update
            await ProcessUpdatedEntityAsync(item);

            //Save changes
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            //Marked as added
            Context.Entry(item).State = EntityState.Added;

            item = await ProcessNewEntityAsync(item);

            //Update the entity relations
            await UpdateRelationsAsync(item);

            //Save db
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(TKey id)
        {
            //Get searched entity 
            var entity = await GetAsync(id);

            //Mark as deleted
            Context.Entry(entity).State = EntityState.Deleted;

            //Process before save
            await ProcessDeletedEntityAsync(entity);

            //Save
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks is an entity exists
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(TKey id)
        {
            var entities = EntitySet.AsNoTracking().Where(IdPredicate(id));
            var searchedEntity = await entities.FirstOrDefaultAsync();

            //Not found
            if (searchedEntity == default)
                return false;

            //Found
            return true;
        }


        //-----------------------------TRAIT INTERFACE----------------------------------
        #region Trait interface
        TContext ICRUDRepositoryTrait<TEntity, TKey, TContext>.Context { get => Context; set => Context = value; }

        DbSet<TEntity> ICRUDRepositoryTrait<TEntity, TKey, TContext>.EntitySet => EntitySet;

        Task<IQueryable<TEntity>> ICRUDRepositoryTrait<TEntity, TKey, TContext>.IncludeRelationsAsync(IQueryable<TEntity> entities)
            => IncludeRelationsAsync(entities);

        Task<TEntity> ICRUDRepositoryTrait<TEntity, TKey, TContext>.UpdateRelationsAsync(TEntity entity)
            => UpdateRelationsAsync(entity);

        Task<TEntity> ICRUDRepositoryTrait<TEntity, TKey, TContext>.ProcessNewEntityAsync(TEntity entity)
            => ProcessNewEntityAsync(entity);

        Task<IQueryable<TEntity>> ICRUDRepositoryTrait<TEntity, TKey, TContext>.ProcessReturnedEntitiesAsync(IQueryable<TEntity> entities)
            => ProcessReturnedEntitiesAsync(entities);

        Task<TEntity> ICRUDRepositoryTrait<TEntity, TKey, TContext>.ProcessUpdatedEntityAsync(TEntity entity)
            => ProcessUpdatedEntityAsync(entity);

        Task<TEntity> ICRUDRepositoryTrait<TEntity, TKey, TContext>.ProcessDeletedEntityAsync(TEntity entity)
            => ProcessDeletedEntityAsync(entity);

        Expression<Func<TEntity, bool>> ICRUDRepositoryTrait<TEntity, TKey, TContext>.IdPredicate(TKey targetId)
            => IdPredicate(targetId);
        #endregion
    }
}
