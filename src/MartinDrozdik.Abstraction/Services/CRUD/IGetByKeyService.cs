using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;

namespace MartinDrozdik.Abstraction.Services.CRUD
{
    public interface IGetByKeyService<TEntity, TKey>
    {
        /// <summary>
        /// Getter for specific instance
        /// </summary>
        /// <param name="id">ID of target instance</param>
        /// <returns>Target instance</returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="DefaultKeyException"></exception>
        Task<TEntity> GetAsync(TKey id);
    }
}
