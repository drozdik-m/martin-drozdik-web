using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Views.Home;
using MartinDrozdik.Web.Views.Projects;

namespace MartinDrozdik.Web.Views.Blog
{
    public class ArticleListModel
    {
        public ArticleListModel(IEnumerable<ArticleTag> tags)
        {
            Tags = tags;
        }

        public IEnumerable<ArticleTag> Tags { get; }
    }

}
