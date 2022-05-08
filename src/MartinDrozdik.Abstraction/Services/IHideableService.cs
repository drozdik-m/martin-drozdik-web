using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;

namespace MartinDrozdik.Abstraction.Services
{
    public interface IHideableService<TModel>
    {
        /// <summary>
        /// Hides an item
        /// </summary>
        /// <param name="itemToHide">The item to hide</param>
        /// <returns>Task</returns>
        /// <exception cref="NotFoundException"></exception>
        Task HideAsync(TModel itemToHide);

        /// <summary>
        /// Shows an item
        /// </summary>
        /// <param name="itemToHide">The item to hide</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task ShowAsync(TModel itemToShow);

        /// <summary>
        /// Toggles visibility of an item
        /// </summary>
        /// <param name="itemToToggle">The item to hide</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task ToggleVisibilityAsync(TModel itemToToggle);
    }
}
