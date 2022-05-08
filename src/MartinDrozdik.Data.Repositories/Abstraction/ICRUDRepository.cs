using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services.CRUD;

namespace MartinDrozdik.Data.Repositories.Abstraction
{
    public interface ICRUDRepository<TEntity, TKey> : ICRUDService<TEntity, TKey>
        where TEntity : IIdentifiable<TKey>
    {
        /// <inheritdoc/>
        Task<bool> ExistsAsync(TKey id);
    }
}
