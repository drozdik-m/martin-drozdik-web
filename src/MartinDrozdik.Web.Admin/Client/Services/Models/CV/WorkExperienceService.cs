using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.CV
{
    public class WorkExperienceService : BaseApiService<WorkExperience, int>,
        ICRUDServiceTrait<WorkExperience, int>,
        IOrderableServiceTrait<WorkExperience, int>,
        ISeedableServiceTrait
    {
        readonly ICRUDServiceTrait<WorkExperience, int> crudTrait;
        readonly IOrderableServiceTrait<WorkExperience, int> orderableTrait;
        readonly ISeedableServiceTrait seedableTrait;

        public WorkExperienceService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
            seedableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/WorkExperience";

        #region CRUD trait

        public Task<IEnumerable<WorkExperience>> GetAsync() => crudTrait.TGetAsync();
        public Task<WorkExperience> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(WorkExperience item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, WorkExperience item) => crudTrait.TUpdateAsync(id, item);

        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion

        #region Seedable trait
        public Task SeedAsync() => seedableTrait.TSeedAsync();
        #endregion
    }
}
