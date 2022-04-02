using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Traits.Abstraction;

namespace MartinDrozdik.Web.Facades
{
    public abstract class CRUDFacade<TEntity, TKey> : ICRUDFacade<TEntity, TKey>, ICRUDFacadeTrait<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>
    {
        readonly ICRUDRepository<TEntity, TKey> repository;
        ICRUDRepository<TEntity, TKey> ICRUDFacadeTrait<TEntity, TKey>.CRUDRepository => repository;


        public CRUDFacade(ICRUDRepository<TEntity, TKey> repository)
        {
            this.repository = repository;
        }

        public virtual Task AddAsync(TEntity item) => repository.AddAsync(item);

        public virtual Task DeleteAsync(TKey id) => repository.DeleteAsync(id);

        public virtual Task<IEnumerable<TEntity>> GetAsync() => repository.GetAsync();

        public virtual Task<TEntity> GetAsync(TKey id) => repository.GetAsync(id);

        public virtual Task UpdateAsync(TKey id, TEntity item) => repository.UpdateAsync(id, item);

        public virtual Task<bool> ExistsAsync(TKey id) => repository.ExistsAsync(id);
    }
}
