using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Abstraction;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.Projects;

namespace MartinDrozdik.Web.Views.Home
{
    public class ArticlePageModel : ViewModelBase
    {
        public Data.Models.Blog.Article Article { get; }

        public ArticlePageModel(ICultureProvider cultureProvider, 
            ILanguageDictionary languageDictionary,
            Data.Models.Blog.Article article)
            : base(cultureProvider, languageDictionary)
        {
            Article = article;
        }


        public override string PageType => "article";

        public override DateTime? RevisionDate => Article.LastEditAt;

        public override string Description => Article.Abstract;

        public override string PageTitle => Article.Name;

        public override IEnumerable<string> KeywordsList => new string[] { 
            Article.Keywords
        }.Concat(Article.Tags.Select(e => e.Tag.Name));

        public override string OgImage => Article.MainImage.Uri;

        public override PageId PageId => PageId.Article;
    }
}
