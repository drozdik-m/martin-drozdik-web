using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.CV
{
    public class LanguageSkillService : BaseApiService<LanguageSkill, int>,
        ICRUDServiceTrait<LanguageSkill, int>,
        IOrderableServiceTrait<LanguageSkill, int>
    {
        readonly ICRUDServiceTrait<LanguageSkill, int> crudTrait;
        readonly IOrderableServiceTrait<LanguageSkill, int> orderableTrait;

        public LanguageSkillService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/LanguageSkill";

        #region CRUD trait

        public Task<IEnumerable<LanguageSkill>> GetAsync() => crudTrait.TGetAsync();
        public Task<LanguageSkill> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(LanguageSkill item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, LanguageSkill item) => crudTrait.TUpdateAsync(id, item);

        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

    }
}
