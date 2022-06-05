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

        protected override Task<IQueryable<ArticleTag>> IncludeRelationsAsync(IQueryable<ArticleTag> entities)
        {
            entities = entities.Include(e => e.Articles);
            return base.IncludeRelationsAsync(entities);
        }
    }
}
