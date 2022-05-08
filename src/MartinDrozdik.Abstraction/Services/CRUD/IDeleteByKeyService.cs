using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;

namespace MartinDrozdik.Abstraction.Services.CRUD
{
    public interface IDeleteByKeyService<TKey>
    {
        /// <summary>
        /// Deletes an instance
        /// </summary>
        /// <param name="id">Target instance</param>
        /// <exception cref="NotFoundException"></exception>
        Task DeleteAsync(TKey id);
    }
}
