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
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.People
{
    public class PersonService : BaseApiService<Person, int>,
        ICRUDServiceTrait<Person, int>,
        IOrderableServiceTrait<Person, int>
    {
        readonly ICRUDServiceTrait<Person, int> crudTrait;
        readonly IOrderableServiceTrait<Person, int> orderableTrait;

        public PersonService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/person";

        #region CRUD trait

        public Task<IEnumerable<Person>> GetAsync() => crudTrait.TGetAsync();
        public Task<Person> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(Person item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, Person item) => crudTrait.TUpdateAsync(id, item);

        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

    }
}
