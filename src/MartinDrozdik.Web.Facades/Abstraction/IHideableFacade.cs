using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;

namespace MartinDrozdik.Web.Facades.Abstraction
{
    public interface IHideableFacade<TEntity, TKey> : IHideableByKeyService<TKey>
        where TEntity : IIdentifiable<TKey>
    {
        Task<IEnumerable<TEntity>> GetVisibleAsync();
    }
}
