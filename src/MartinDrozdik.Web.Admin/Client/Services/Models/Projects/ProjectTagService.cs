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
    public class ProjectTagService : BaseApiService<ProjectTag, int>,
        ICRUDServiceTrait<ProjectTag, int>, 
        IOrderableServiceTrait<ProjectTag, int>
    {
        readonly ICRUDServiceTrait<ProjectTag, int> crudTrait;
        readonly IOrderableServiceTrait<ProjectTag, int> orderableTrait;

        public ProjectTagService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/projectTag";

        #region CRUD trait

        public Task<IEnumerable<ProjectTag>> GetAsync() => crudTrait.TGetAsync();
        public Task<ProjectTag> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(ProjectTag item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, ProjectTag item) => crudTrait.TUpdateAsync(id, item);

        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

    }
}
