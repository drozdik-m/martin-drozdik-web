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

namespace Bonsai.DataPersistence.Repositories.Traits
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
        public new async Task<IEnumerable<TEntity>> GetVisibleAsync()
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

            var entities = EntitySet.Where(IdPredicate(itemToModify));
            var searchedEntity = await entities.FirstOrDefaultAsync();

            //Not found
            if (searchedEntity == default)
                throw new NotFoundException();

            //Hide
            searchedEntity.IsHidden = newValueGetter.Invoke(searchedEntity.IsHidden);

            //Set modified state
            Context.Entry(searchedEntity).State = EntityState.Modified;

            //Process before update
            await ProcessUpdatedEntityAsync(searchedEntity);

            //Save changes
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Hides an item
        /// </summary>
        /// <param name="itemToHide"></param>
        /// <returns></returns>
        public new Task HideAsync(TKey itemToHide) => SetHidden(itemToHide, _ => true);

        /// <summary>
        /// Shows an item
        /// </summary>
        /// <param name="itemToShow"></param>
        /// <returns></returns>
        public new Task ShowAsync(TKey itemToShow) => SetHidden(itemToShow, _ => false);

        /// <summary>
        /// Toggles items' visibility
        /// </summary>
        /// <param name="itemToToggle"></param>
        /// <returns></returns>
        public new Task ToggleVisibilityAsync(TKey itemToToggle) => SetHidden(itemToToggle, e => !e);
    }
}
