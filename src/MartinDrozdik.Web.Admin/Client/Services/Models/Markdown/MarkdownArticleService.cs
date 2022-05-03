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
using MartinDrozdik.Data.Models.Markdown.Media;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Models.Services;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.Media
{
    public abstract class MarkdownArticleService<TArticle> : BaseApiService<TArticle, int>,
        IMarkdownArticleService<TArticle, int>,
        ICRUDServiceTrait<TArticle, int>
        where TArticle : MarkdownArticle
    {
        readonly ICRUDServiceTrait<TArticle, int> crudTrait;

        public MarkdownArticleService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
        }

        /// <inheritdoc />
        public async Task<AddFileResponse> UploadFileAsync(int id, UploadFileData file)
        {
            var response = await Http.PostAsync($"{ApiUri}/{id}/file", file.ToJsonContent());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<AddFileResponse>();
        }

        /// <inheritdoc />
        public async Task<AddMediaResponse> UploadRegularImageAsync(int id, UploadFileData file)
        {
            var response = await Http.PostAsync($"{ApiUri}/{id}/image", file.ToJsonContent());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<AddMediaResponse>();
        }

        /// <inheritdoc />
        public async Task<AddMediaResponse> UploadTextWidthImageAsync(int id, UploadFileData file)
        {
            var response = await Http.PostAsync($"{ApiUri}/{id}/textwidth-image", file.ToJsonContent());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<AddMediaResponse>();
        }

        /// <inheritdoc />
        public async Task<AddMediaResponse> UploadWideImageAsync(int id, UploadFileData file)
        {
            var response = await Http.PostAsync($"{ApiUri}/{id}/wide-image", file.ToJsonContent());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<AddMediaResponse>();
        }

        #region CRUD trait

        public Task<IEnumerable<TArticle>> GetAsync() => crudTrait.TGetAsync();
        public Task<TArticle> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(TArticle item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, TArticle item) => crudTrait.TUpdateAsync(id, item);

        #endregion
    }
}
