using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.Repositories.Traits.Abstraction;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Traits.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.AppLogic.Facades.Traits
{
    interface IOrderableFacadeTrait<TEntity, TKey>
            : ICRUDFacade<TEntity, TKey>, ICRUDFacadeTrait<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>, IOrderable
    {
        protected abstract IOrderableRepository<TEntity, TKey> OrderableRepository { get; }

        public Task TReorderAsync(IEnumerable<TKey> newOrder) => OrderableRepository.ReorderAsync(newOrder);
    }
}
