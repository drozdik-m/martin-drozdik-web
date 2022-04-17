using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Services;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.Projects
{
    public class ProjectService : BaseApiService<Project, int>,
        ICRUDServiceTrait<Project, int>, 
        IOrderableServiceTrait<Project, int>
    {
        readonly ICRUDServiceTrait<Project, int> crudTrait;
        readonly IOrderableServiceTrait<Project, int> orderableTrait;

        public ProjectService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/project";

        #region CRUD trait

        public Task<IEnumerable<Project>> GetAsync() => crudTrait.TGetAsync();
        public Task<Project> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(Project item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, Project item) => crudTrait.TUpdateAsync(id, item);

        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

    }
}
