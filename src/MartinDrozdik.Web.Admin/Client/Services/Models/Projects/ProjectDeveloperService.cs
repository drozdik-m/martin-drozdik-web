using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Services;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.Projects
{
    public class ProjectDeveloperService : BaseApiService<ProjectDeveloper, int>,
        ICRUDServiceTrait<ProjectDeveloper, int>, 
        IOrderableServiceTrait<ProjectDeveloper, int>
    {
        readonly ICRUDServiceTrait<ProjectDeveloper, int> crudTrait;
        readonly IOrderableServiceTrait<ProjectDeveloper, int> orderableTrait;

        public ProjectDeveloperService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/ProjectDeveloper";

        #region CRUD trait
        public Task<IEnumerable<ProjectDeveloper>> GetAsync() => crudTrait.TGetAsync();
        public Task<ProjectDeveloper> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(ProjectDeveloper item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, ProjectDeveloper item) => crudTrait.TUpdateAsync(id, item);
        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion
    }
}
