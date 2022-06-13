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
    public class EducationService : BaseApiService<Education, int>,
        ICRUDServiceTrait<Education, int>,
        IOrderableServiceTrait<Education, int>,
        ISeedableServiceTrait
    {
        readonly ICRUDServiceTrait<Education, int> crudTrait;
        readonly IOrderableServiceTrait<Education, int> orderableTrait;
        readonly ISeedableServiceTrait seedableTrait;

        public EducationService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
            seedableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/Education";

        #region CRUD trait

        public Task<IEnumerable<Education>> GetAsync() => crudTrait.TGetAsync();
        public Task<Education> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(Education item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, Education item) => crudTrait.TUpdateAsync(id, item);

        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion

        #region Seedable trait
        public Task SeedAsync() => seedableTrait.TSeedAsync();
        #endregion
    }
}
