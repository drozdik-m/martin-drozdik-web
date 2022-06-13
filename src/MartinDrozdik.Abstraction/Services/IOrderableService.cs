using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Abstraction.Services
{
    public interface IOrderableService<TKey>
    {
        /// <summary>
        /// Consumes enumrable of keys, which indicate the new order
        /// </summary>
        /// <param name="newOrder">Collection of keys</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task ReorderAsync(IEnumerable<TKey> newOrder);
    }
}
