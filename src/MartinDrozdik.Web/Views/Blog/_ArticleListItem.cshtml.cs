using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Views.Home;

namespace MartinDrozdik.Web.Views.Blog
{
    public class ArticleListItemModel
    {
        public ArticleListItemModel(Article article)
        {
            Article = article;
        }

        public Article Article { get; }
    }
}
