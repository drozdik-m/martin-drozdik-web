using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.People
{
    public class TechnologyService : BaseApiService<Technology, int>,
        ICRUDServiceTrait<Technology, int>,
        IOrderableServiceTrait<Technology, int>
    {
        readonly ICRUDServiceTrait<Technology, int> crudTrait;
        readonly IOrderableServiceTrait<Technology, int> orderableTrait;

        public TechnologyService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/technology";

        #region CRUD trait

        public Task<IEnumerable<Technology>> GetAsync() => crudTrait.TGetAsync();
        public Task<Technology> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(Technology item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, Technology item) => crudTrait.TUpdateAsync(id, item);

        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

    }
}
