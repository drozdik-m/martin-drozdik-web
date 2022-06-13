using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;

namespace MartinDrozdik.Abstraction.Services
{
    public interface IHideableByKeyService<TKey>
    {
        /// <summary>
        /// Hides an item
        /// </summary>
        /// <param name="itemToHide">The id of the item</param>
        /// <returns>Task</returns>
        /// <exception cref="NotFoundException"></exception>
        Task HideAsync(TKey itemToHide);

        /// <summary>
        /// Shows an item
        /// </summary>
        /// <param name="itemToHide">The id of the item</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task ShowAsync(TKey itemToShow);

        /// <summary>
        /// Toggles visibility of an item
        /// </summary>
        /// <param name="itemToToggle">The id of the item</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task ToggleVisibilityAsync(TKey itemToToggle);
    }
}
