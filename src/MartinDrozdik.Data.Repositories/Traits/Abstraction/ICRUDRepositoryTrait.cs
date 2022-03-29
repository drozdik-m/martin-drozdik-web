using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.DataPersistence.Repositories.Traits.Abstraction
{
    interface ICRUDRepositoryTrait<TEntity, TKey, TContext> : ICRUDRepository<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>
        where TContext : DbContext
    {
        protected TContext Context { get; set; }

        protected abstract DbSet<TEntity> EntitySet { get; }

        protected Task<IQueryable<TEntity>> TIncludeRelationsAsync(IQueryable<TEntity> entities);

        protected Task<TEntity> TUpdateRelationsAsync(TEntity entity);

        protected Task<TEntity> TProcessNewEntityAsync(TEntity entity);

        protected Task<IQueryable<TEntity>> TProcessReturnedEntitiesAsync(IQueryable<TEntity> entities);

        protected Task<TEntity> TProcessUpdatedEntityAsync(TEntity entity);

        protected Task<TEntity> TProcessDeletedEntityAsync(TEntity entity);

        protected Expression<Func<TEntity, bool>> TIdPredicate(TKey targetId);
    }
}
