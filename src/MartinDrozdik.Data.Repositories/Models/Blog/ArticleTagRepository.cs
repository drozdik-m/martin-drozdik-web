using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.DbContexts;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.Blog
{
    public class ArticleTagRepository : TagRepository<ArticleTag>
    {
        public ArticleTagRepository(AppDb context) : base(context)
        {
        }

        protected override DbSet<ArticleTag> EntitySet => Context.ArticleTags;

    }
}
