using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.Repositories.Abstraction;
using Bonsai.DataPersistence.Repositories.Traits.Abstraction;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
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
        internal IQueryable<TEntity> TOrder(IQueryable<TEntity> entities)
        {
            entities = entities.OrderBy(c => c.OrderIndex);
            return entities;
        }

        public async Task TReorderAsync(IEnumerable<TKey> newOrder)
        {
            if (newOrder == null)
                throw new ArgumentNullException(nameof(newOrder));

            var i = 1;
            foreach (var id in newOrder)
            {
                var entityToUpdate = await EntitySet.FindAsync(id);
                entityToUpdate.OrderIndex = i++;
                Context.Entry(entityToUpdate).State = EntityState.Modified;
            }

            await Context.SaveChangesAsync();
        }
    }
}
