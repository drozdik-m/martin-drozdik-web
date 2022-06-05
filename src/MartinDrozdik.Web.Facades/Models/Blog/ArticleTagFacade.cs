using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Blog;
using MartinDrozdik.Data.Repositories.Models.Projects;
using MartinDrozdik.Web.Facades.Models.Tags;

namespace MartinDrozdik.Web.Facades.Models.Blog
{
    public class ArticleTagFacade : TagFacade<ArticleTag>
    {
        public ArticleTagFacade(ArticleTagRepository repository) : base(repository)
        {
        }
    }
}
