using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using Bonsai.DataPersistence.Repositories.Blog;
using MartinDrozdik.Data.Models.Tags.Projects;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.Tags.Projects
{
    public class ProjectTagRepository : TagRepository<ProjectTag>
    {
        public ProjectTagRepository(AppDb context) : base(context)
        {
        }

        protected override DbSet<ProjectTag> EntitySet => Context.ProjectTags;
    }
}
