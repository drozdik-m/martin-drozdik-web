using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Data.Repositories.Traits;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.CV
{
    public class EducationRepository : CRUDRepository<Education, int, AppDb>,
        IOrderableRepositoryTrait<Education, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<Education, int, AppDb> orderableTrait;

        public EducationRepository(AppDb context) : base(context)
        {
            orderableTrait = this;
        }

        protected override DbSet<Education> EntitySet => Context.Educations;

        protected override Expression<Func<Education, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;


        protected override async Task<Education> ProcessNewEntityAsync(Education entity)
        {
            entity = await base.ProcessNewEntityAsync(entity);
            entity = await orderableTrait.SetInitialOrderAsync(entity);
            return entity;
        }

        protected override async Task<IQueryable<Education>> ProcessReturnedEntitiesAsync(IQueryable<Education> entities)
        {
            entities = await base.ProcessReturnedEntitiesAsync(entities);
            return orderableTrait.Order(entities);
        }

        #region Orderable trait

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion
    }
}
