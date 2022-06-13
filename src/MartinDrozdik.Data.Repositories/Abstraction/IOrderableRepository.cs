using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services;

namespace MartinDrozdik.Data.Repositories.Abstraction
{
    public interface IOrderableRepository<TEntity, TKey> : IOrderableService<TKey>
        where TEntity : IIdentifiable<TKey>
    {
    }
}
