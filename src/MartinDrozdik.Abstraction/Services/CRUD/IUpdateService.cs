using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;

namespace MartinDrozdik.Abstraction.Services.CRUD
{
    public interface IUpdateService<TEntity, TKey>
    {
        /// <summary>
        /// Updates existing instance
        /// </summary>
        /// <param name="id">Target instance</param>
        /// <param name="item">Modified instance</param>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="DifferentIDsException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task UpdateAsync(TKey id, TEntity item);
    }
}
