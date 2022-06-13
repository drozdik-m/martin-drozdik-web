using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Utils.JSON;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services.CRUD;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Traits
{
    interface ICRUDServiceTrait<TEntity, TKey> : IServiceTrait, ICRUDService<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>
    {

        public async Task<IEnumerable<TEntity>> TGetAsync()
        {
            var response = await Http.GetAsync(ApiUri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<List<TEntity>>();
        }

        public async Task<TEntity> TGetAsync(TKey id)
        {
            var response = await Http.GetAsync($"{ApiUri}/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<TEntity>();
        }

        public async Task TAddAsync(TEntity item)
        {
            var response = await Http.PostAsync(ApiUri, item.ToJsonContent());
            response.EnsureSuccessStatusCode();
        }

        public async Task TDeleteAsync(TKey id)
        {
            var response = await Http.DeleteAsync($"{ApiUri}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task TUpdateAsync(TKey id, TEntity item)
        {
            var response = await Http.PutAsync($"{ApiUri}/{id}", item.ToJsonContent());
            response.EnsureSuccessStatusCode();
        }
    }
}
