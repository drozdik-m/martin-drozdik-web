using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services;

namespace MartinDrozdik.Data.Repositories.Abstraction
{
    public interface ILocalizedRepository<TEntity, TKey> : ILocalizedService<TEntity>
        where TEntity : ILocalizable
    {

    }
}
