using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.Blog
{
    public class ArticleTagService : BaseApiService<ArticleTag, int>,
        ICRUDServiceTrait<ArticleTag, int>,
        IOrderableServiceTrait<ArticleTag, int>
    {
        readonly ICRUDServiceTrait<ArticleTag, int> crudTrait;
        readonly IOrderableServiceTrait<ArticleTag, int> orderableTrait;

        public ArticleTagService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/articleTag";

        #region CRUD trait

        public Task<IEnumerable<ArticleTag>> GetAsync() => crudTrait.TGetAsync();
        public Task<ArticleTag> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(ArticleTag item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, ArticleTag item) => crudTrait.TUpdateAsync(id, item);

        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

    }
}
