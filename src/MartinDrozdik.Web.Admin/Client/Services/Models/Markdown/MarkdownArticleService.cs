using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using Bonsai.Utils.JSON;
using MartinDrozdik.Data.Models.Files;
using MartinDrozdik.Data.Models.Markdown;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Models.Services;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.Media
{
    public abstract class MarkdownArticleService<TArticle> : BaseApiService<TArticle, int>,
        ICRUDServiceTrait<TArticle, int>
        where TArticle : MarkdownArticle
    {
        readonly ICRUDServiceTrait<TArticle, int> crudTrait;

        public MarkdownArticleService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
        }

        /*public async Task AddMediaAsync(int id, UploadFileData image)
        {
            var response = await Http.PostAsync($"{ApiUri}/{id}/media", image.ToJsonContent());
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMediaAsync(int id)
        {
            var response = await Http.DeleteAsync($"{ApiUri}/{id}/media");
            response.EnsureSuccessStatusCode();
        }*/

        #region CRUD trait

        public Task<IEnumerable<TArticle>> GetAsync() => crudTrait.TGetAsync();
        public Task<TArticle> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(TArticle item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, TArticle item) => crudTrait.TUpdateAsync(id, item);

        #endregion
    }
}
