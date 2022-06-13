using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Models.Abstraction.Services.CRUD
{
    public interface IGetAllService<TEntity>
    {
        /// <summary>
        /// Getter for all instances
        /// </summary>
        /// <returns>IEnumerable object of all instances</returns>
        Task<IEnumerable<TEntity>> GetAsync();
    }
}
