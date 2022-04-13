using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Data.Repositories.Models.Projects
{
    public class ProjectTagRepository : TagRepository<ProjectTag>
    {
        public ProjectTagRepository(AppDb context) : base(context)
        {
        }

        protected override DbSet<ProjectTag> EntitySet => Context.ProjectTags;

        protected override Task<IQueryable<ProjectTag>> IncludeRelationsAsync(IQueryable<ProjectTag> entities)
        {
            entities = entities.Include(e => e.Projects);
            return base.IncludeRelationsAsync(entities);
        }
    }
}
