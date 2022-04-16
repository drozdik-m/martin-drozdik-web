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
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Models.Services;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.Media
{
    public abstract class MediaBaseService<TMedia> : BaseApiService<TMedia, int>, IMediaService<int>,
        ICRUDServiceTrait<TMedia, int>
        where TMedia : MediaBase
    {
        readonly ICRUDServiceTrait<TMedia, int> crudTrait;

        public MediaBaseService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
        }

        public async Task AddMediaAsync(int id, UploadFileData image)
        {
            var response = await Http.PostAsync($"{ApiUri}/{id}/media", image.ToJsonContent());
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMediaAsync(int id)
        {
            var response = await Http.DeleteAsync($"{ApiUri}/{id}/media");
            response.EnsureSuccessStatusCode();
        }

        #region CRUD trait

        public Task<IEnumerable<TMedia>> GetAsync() => crudTrait.TGetAsync();
        public Task<TMedia> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(TMedia item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, TMedia item) => crudTrait.TUpdateAsync(id, item);

        #endregion
    }
}
