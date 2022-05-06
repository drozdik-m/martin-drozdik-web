using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Localization;
using MartinDrozdik.Data.Models.Tags;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Traits;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Media;
using MartinDrozdik.Data.DbContexts;

namespace MartinDrozdik.Data.Repositories.Models.Projects
{
    public class ProjectMarkdownArticleRepository : MarkdownArticleRepository<ProjectMarkdownArticle>
    {
        public ProjectMarkdownArticleRepository(AppDb context)
            : base(context)
        {
            
        }

        protected override DbSet<ProjectMarkdownArticle> EntitySet => Context.ProjectMarkdownArticles;
    }
}
