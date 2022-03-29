using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.Repositories.Traits.Abstraction;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using MartinDrozdik.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.DataPersistence.Repositories.Traits
{
    interface ILocalizedRepositoryTrait<TEntity, TKey, TContext>
        : ICRUDRepository<TEntity, TKey>, ICRUDRepositoryTrait<TEntity, TKey, TContext>
        where TEntity : class, IIdentifiable<TKey>, ILocalizable
        where TContext : DbContext
    {
        public Task<IEnumerable<TEntity>> TGetForLanguageAsync(ILanguage language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            return TGetForLanguageAsync(language.Id);
        }

        public async Task<IEnumerable<TEntity>> TGetForLanguageAsync(int languageId)
        {
            var allEntities = await TIncludeRelationsAsync(EntitySet);
            var processedEntities = await TProcessReturnedEntitiesAsync(allEntities);
            return await processedEntities
                .Include(e => e.Language)
                .Where(e => e.Language != null)
                .Where(e => e.Language.Id == languageId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> TGetWithoutLanguageAsync()
        {
            var allEntities = await TIncludeRelationsAsync(EntitySet);
            var processedEntities = await TProcessReturnedEntitiesAsync(allEntities);
            return await processedEntities
                .Where(e => e.Language == null)
                .ToListAsync();
        }
    }
}
