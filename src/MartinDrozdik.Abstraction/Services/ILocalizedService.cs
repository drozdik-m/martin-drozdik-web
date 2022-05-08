using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Localization;

namespace MartinDrozdik.Abstraction.Services
{
    public interface ILocalizedService<TEntity>
    {
        /// <summary>
        /// Returns IEnumerable of entities with given language
        /// </summary>
        /// <param name="language">The language</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetForLanguageAsync(ILanguage language);

        /// <summary>
        /// Returns IEnumerable of entities with given language
        /// </summary>
        /// <param name="language">The language id</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetForLanguageAsync(int languageId);

        /// <summary>
        /// Returns entities that do not have any language
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetWithoutLanguageAsync();
    }
}
