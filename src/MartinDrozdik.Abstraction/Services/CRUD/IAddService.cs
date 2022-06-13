using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;

namespace MartinDrozdik.Abstraction.Services.CRUD
{
    public interface IAddService<TEntity>
    {
        /// <summary>
        /// Creates new instance
        /// </summary>
        /// <param name="item">New item</param>
        /// <exception cref="AlreadyExistsException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task AddAsync(TEntity item);
    }
}
