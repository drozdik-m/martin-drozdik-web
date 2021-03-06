using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MartinDrozdik.Data.Repositories.Traits;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.DbContexts;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Repositories.Models.Projects;

namespace MartinDrozdik.Data.Repositories.Models.Blog
{
    public class ArticleMainImageRepository : ArticleImageRepository<ArticleMainImage>
    {

        public ArticleMainImageRepository(AppDb context)
            : base(context)
        {
            
        }

        protected override DbSet<ArticleMainImage> EntitySet => Context.ArticleMainImages;
    }
}
