using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;
using MartinDrozdik.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Traits
{
    interface IHideableRepositoryTrait<TEntity, TKey, TContext>
        : ICRUDRepositoryTrait<TEntity, TKey, TContext>, IHideableRepository<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>, IHideable
        where TContext : DbContext
    {
        /// <summary>
        /// Returns all visible items
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> TGetVisibleAsync()
        {
            var allEntities = await IncludeRelationsAsync(EntitySet);
            var shownEntities = allEntities.Where(e => !e.IsHidden);
            var processedEntities = await ProcessReturnedEntitiesAsync(shownEntities);
            return await processedEntities.ToListAsync();
        }

        /// <summary>
        /// Sets a visibility for an item
        /// </summary>
        /// <param name="itemToModify"></param>
        /// <param name="newValueGetter"></param>
        /// <returns></returns>
        /// <exception cref="DefaultKeyException"></exception>
        /// <exception cref="NotFoundException"></exception>
        private async Task SetHidden(TKey itemToModify, Func<bool, bool> newValueGetter)
        {
            if (Equals(itemToModify, default(TKey)))
                throw new DefaultKeyException();

            var entities = EntitySet
                .Where(IdPredicate(itemToModify))
                .AsNoTracking();
            var searchedEntity = await entities.FirstOrDefaultAsync();

            //Not found
            if (searchedEntity == default)
                throw new NotFoundException();

            //Hide
            var newValue = newValueGetter.Invoke(searchedEntity.IsHidden);
            searchedEntity.IsHidden = newValue;

            //Set modified state
            Context.Entry(searchedEntity).State = EntityState.Modified;

            //Save changes
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Hides an item
        /// </summary>
        /// <param name="itemToHide"></param>
        /// <returns></returns>
        public Task THideAsync(TKey itemToHide) => SetHidden(itemToHide, _ => true);

        /// <summary>
        /// Shows an item
        /// </summary>
        /// <param name="itemToShow"></param>
        /// <returns></returns>
        public Task TShowAsync(TKey itemToShow) => SetHidden(itemToShow, _ => false);

        /// <summary>
        /// Toggles items' visibility
        /// </summary>
        /// <param name="itemToToggle"></param>
        /// <returns></returns>
        public Task TToggleVisibilityAsync(TKey itemToToggle) => SetHidden(itemToToggle, e => !e);
    }
}
