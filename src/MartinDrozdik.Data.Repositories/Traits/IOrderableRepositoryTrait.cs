using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;
using MartinDrozdik.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Traits
{
    interface IOrderableRepositoryTrait<TEntity, TKey, TContext>
            : ICRUDRepositoryTrait<TEntity, TKey, TContext>, IOrderableRepository<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>, IOrderable
        where TContext : DbContext
    {
        /// <summary>
        /// Orders entities by their order index
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Ordered entities</returns>
        internal IQueryable<TEntity> Order(IQueryable<TEntity> entities)
        {
            entities = entities.OrderBy(c => c.OrderIndex);
            return entities;
        }

        /// <summary>
        /// Sets the initial order index for an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal async Task<TEntity> SetInitialOrderAsync(TEntity entity)
        {
            var entityCount = await EntitySet.CountAsync();
            if (entityCount == 0)
                entity.OrderIndex = 0;
            else
            {
                var maxOrder = await EntitySet.MaxAsync(e => e.OrderIndex);
                entity.OrderIndex = maxOrder + 1;
            }

            return entity;
        }

        /// <summary>
        /// Orders items in the same order as in the recieved array.
        /// The array can be a subset of all entities and orders from one to n.
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task TReorderAsync(IEnumerable<TKey> newOrder)
        {
            if (newOrder == null)
                throw new ArgumentNullException(nameof(newOrder));

            var i = 0;
            foreach (var id in newOrder)
            {
                if (Equals(id, default(TKey)))
                    throw new DefaultKeyException();

                var entities = EntitySet
                    .AsNoTracking()
                    .Where(IdPredicate(id));
                //var includedEntities = await IncludeRelationsAsync(entities);
                //var processedEntities = await ProcessReturnedEntitiesAsync(includedEntities);
                var searchedEntity = await entities.FirstOrDefaultAsync();

                //Not found
                if (searchedEntity == default)
                    throw new NotFoundException();

                //Update order index
                searchedEntity.OrderIndex = i++;

                //Set modified state
                Context.Entry(searchedEntity).State = EntityState.Modified;

                //Process before update
                //await ProcessUpdatedEntityAsync(searchedEntity);

                //Save changes
                await Context.SaveChangesAsync();
            }

            await Context.SaveChangesAsync();
        }
    }
}
