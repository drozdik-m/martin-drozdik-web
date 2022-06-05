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
    public class ArticleService : BaseApiService<Article, int>,
        ICRUDServiceTrait<Article, int>,
        IOrderableServiceTrait<Article, int>,
        IHideableServiceTrait<Article, int>
    {
        readonly ICRUDServiceTrait<Article, int> crudTrait;
        readonly IOrderableServiceTrait<Article, int> orderableTrait;
        readonly IHideableServiceTrait<Article, int> hideableTrait;

        public ArticleService(HttpClient http)
            : base(http)
        {
            crudTrait = this;
            orderableTrait = this;
            hideableTrait = this;
        }

        protected override string ApiUri { get; set; } = "/api/article";

        #region CRUD trait
        public Task<IEnumerable<Article>> GetAsync() => crudTrait.TGetAsync();
        public Task<Article> GetAsync(int id) => crudTrait.TGetAsync(id);
        public Task AddAsync(Article item) => crudTrait.TAddAsync(item);
        public Task DeleteAsync(int id) => crudTrait.TDeleteAsync(id);
        public Task UpdateAsync(int id, Article item) => crudTrait.TUpdateAsync(id, item);
        #endregion

        #region Orderable trait
        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion

        #region Hideable trait
        public Task HideAsync(Article itemToHide) => hideableTrait.THideAsync(itemToHide);
        public Task ShowAsync(Article itemToShow) => hideableTrait.TShowAsync(itemToShow);
        public Task ToggleVisibilityAsync(Article itemToToggle) => hideableTrait.TToggleVisibilityAsync(itemToToggle);
        #endregion
    }
}
