using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Localization;
using MartinDrozdik.Data.Repositories;
using MartinDrozdik.Data.Models.Tags;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Traits;
using MartinDrozdik.Data.DbContexts;

namespace MartinDrozdik.Data.Repositories.Models.Tags
{
    public abstract class TagRepository<TTag> : CRUDRepository<TTag, int, AppDb>,
        IOrderableRepositoryTrait<TTag, int, AppDb>
        where TTag : Tag
    {
        readonly IOrderableRepositoryTrait<TTag, int, AppDb> orderableTrait;

        public TagRepository(AppDb context)
            : base(context)
        {
            orderableTrait = this;
        }

        protected override Expression<Func<TTag, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        protected override async Task<TTag> ProcessNewEntityAsync(TTag entity)
        {
            entity = await base.ProcessNewEntityAsync(entity);
            entity = await orderableTrait.SetInitialOrderAsync(entity);
            return entity;
        }

        protected override async Task<IQueryable<TTag>> ProcessReturnedEntitiesAsync(IQueryable<TTag> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            return orderableTrait.Order(entities);
        }

        #region Orderable trait

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
