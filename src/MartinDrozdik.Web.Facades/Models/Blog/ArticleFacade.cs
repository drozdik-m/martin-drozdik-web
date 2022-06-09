using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Blog;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Data.Repositories.Models.Projects;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Projects;
using MartinDrozdik.Web.Facades.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;

namespace MartinDrozdik.Web.Facades.Models.Blog
{
    public class ArticleFacade : CRUDFacade<Article, int>,
        IHideableFacadeTrait<Article, int>
    {
        readonly IHideableFacadeTrait<Article, int> hideableTrait;

        readonly ArticleRepository repository;
        readonly ArticleMainImageFacade mainImageFacade;
        readonly BlogMarkdownArticleFacade contentFacade;

        public ArticleFacade(ArticleRepository repository,
            ArticleMainImageFacade mainImageFacade,
            BlogMarkdownArticleFacade contentFacade) : base(repository)
        {
            hideableTrait = this;
            this.repository = repository;
            this.mainImageFacade = mainImageFacade;
            this.contentFacade = contentFacade;
        }


        public Task<Article> GetAsync(string id) => repository.GetAsync(id);

        public Task<IEnumerable<Article>> GetPublishedAsync() => repository.GetPublishedAsync();

        public Task<IEnumerable<Article>> GetFirstPublishedAsync(int count) => repository.GetFirstPublishedAsync(count);

        public override async Task DeleteAsync(int id)
        {
            var item = await repository.GetAsync(id);

            contentFacade.DisposeContents(item.Content);

            await mainImageFacade.DeleteMediaAsync(item.MainImage);
            mainImageFacade.DisposeContentFolder(item.MainImage);

            await base.DeleteAsync(id);
        }

        public override async Task UpdateAsync(int id, Article item)
        {
            //Content update
            contentFacade.UpdateArticlesContent(item.Content);

            await base.UpdateAsync(id, item);
        }

        #region Hideable trait
        IHideableRepository<Article, int> IHideableFacadeTrait<Article, int>.HideableRepository => repository;
        public Task<IEnumerable<Article>> GetVisibleAsync() => hideableTrait.TGetVisibleAsync();
        public Task HideAsync(int itemToHide) => hideableTrait.THideAsync(itemToHide);
        public Task ShowAsync(int itemToShow) => hideableTrait.TShowAsync(itemToShow);
        public Task ToggleVisibilityAsync(int itemToToggle) => hideableTrait.TToggleVisibilityAsync(itemToToggle);
        #endregion
    }
}
