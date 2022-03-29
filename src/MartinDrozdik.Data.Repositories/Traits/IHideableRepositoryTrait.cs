using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.Repositories.Traits.Abstraction;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using MartinDrozdik.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.DataPersistence.Repositories.Traits
{
    interface IHideableRepositoryTrait<TEntity, TKey, TContext>
        : ICRUDRepositoryTrait<TEntity, TKey, TContext>
        where TEntity : class, IIdentifiable<TKey>, IHideable
        where TContext : DbContext
    {
        public async Task<IEnumerable<TEntity>> TGetVisibleAsync()
        {
            var allEntities = await TIncludeRelationsAsync(EntitySet);
            var shownEntities = allEntities.Where(e => !e.IsHidden);
            var processedEntities = await TProcessReturnedEntitiesAsync(shownEntities);
            return await processedEntities.ToListAsync();
        }

        public async Task THideAsync(TKey itemToHide)
        {
            var entity = await GetAsync(itemToHide);
            entity.IsHidden = true;
            await UpdateAsync(entity.Id, entity);
        }

        public async Task TShowAsync(TKey itemToShow)
        {
            var entity = await GetAsync(itemToShow);
            entity.IsHidden = false;
            await UpdateAsync(entity.Id, entity);
        }

        public async Task TToggleVisibilityAsync(TKey itemToToggle)
        {
            var entity = await GetAsync(itemToToggle);
            entity.IsHidden = !entity.IsHidden;
            await UpdateAsync(entity.Id, entity);
        }
    }
}
