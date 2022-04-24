using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Data.Repositories.Models.Projects;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;

namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class ProjectDeveloperFacade : CRUDFacade<ProjectDeveloper, int>,
        IOrderableFacadeTrait<ProjectDeveloper, int>
    {
        readonly IOrderableFacadeTrait<ProjectDeveloper, int> orderableTrait;

        readonly ProjectDeveloperRepository repository;

        public ProjectDeveloperFacade(ProjectDeveloperRepository repository) : base(repository)
        {
            orderableTrait = this;
            this.repository = repository;
        }

        #region Orderable trait
        IOrderableRepository<ProjectDeveloper, int> IOrderableFacadeTrait<ProjectDeveloper, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion
    }
}
