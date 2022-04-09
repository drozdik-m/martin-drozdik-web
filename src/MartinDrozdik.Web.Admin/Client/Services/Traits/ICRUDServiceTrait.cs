using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using Bonsai.Models.Exceptions.CRUD;
using Bonsai.Utils.JSON;
using Newtonsoft.Json;

namespace Bonsai.RazorPages.Admin.Services.Traits
{
    interface ICRUDServiceTrait<TEntity, TKey> : ICRUDService<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>
    {
        protected abstract string ApiUri { get; }

        protected abstract HttpClient Http { get; }

        public new async Task<IEnumerable<TEntity>> GetAsync()
        {
            var response = await Http.GetAsync(ApiUri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<List<TEntity>>();
        }

        public new async Task<TEntity> GetAsync(TKey id)
        {
            var response = await Http.GetAsync($"{ApiUri}/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<TEntity>();
        }

        public new async Task AddAsync(TEntity item)
        {
            var response = await Http.PostAsync(ApiUri, item.ToJsonContent());
            response.EnsureSuccessStatusCode();
        }

        public new async Task DeleteAsync(TKey id)
        {
            var response = await Http.GetAsync($"{ApiUri}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public new async Task UpdateAsync(TKey id, TEntity item)
        {
            var response = await Http.PostAsync($"{ApiUri}/{id}", item.ToJsonContent());
            response.EnsureSuccessStatusCode();
        }
    }
}
