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
using Microsoft.EntityFrameworkCore;

namespace Bonsai.DataPersistence.Repositories.Traits
{
    interface IOrderableRepositoryTrait<TEntity, TKey, TContext>
            : ICRUDRepositoryTrait<TEntity, TKey, TContext>
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
        /// Orders items in the same order as in the recieved array.
        /// The array can be a subset of all entities and orders from one to n.
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task ReorderAsync(IEnumerable<TKey> newOrder)
        {
            if (newOrder == null)
                throw new ArgumentNullException(nameof(newOrder));

            var i = 1;
            foreach (var id in newOrder)
            {
                if (Equals(id, default(TKey)))
                    throw new DefaultKeyException();

                var entities = EntitySet.Where(IdPredicate(id));
                var searchedEntity = await entities.FirstOrDefaultAsync();

                //Not found
                if (searchedEntity == default)
                    throw new NotFoundException();

                //Update order index
                searchedEntity.OrderIndex = i++;

                //Set modified state
                Context.Entry(searchedEntity).State = EntityState.Modified;

                //Process before update
                await ProcessUpdatedEntityAsync(searchedEntity);

                //Save changes
                await Context.SaveChangesAsync();
            }

            await Context.SaveChangesAsync();
        }
    }
}
