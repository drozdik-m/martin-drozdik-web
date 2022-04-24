using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Data.Repositories.Traits;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.Projects
{
    public class ProjectDeveloperRepository : CRUDRepository<ProjectDeveloper, int, AppDb>,
        IOrderableRepositoryTrait<ProjectDeveloper, int, AppDb>
    {
        readonly IOrderableRepositoryTrait<ProjectDeveloper, int, AppDb> orderableTrait;

        public ProjectDeveloperRepository(AppDb context) : base(context)
        {
            orderableTrait = this;
        }

        protected override DbSet<ProjectDeveloper> EntitySet => Context.ProjectDevelopers;

        protected override Expression<Func<ProjectDeveloper, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion
    }
}
