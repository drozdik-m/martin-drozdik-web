using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.Repositories.Blog;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Facades.Models.Tags;

namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class ProjectTagFacade : TagFacade<ProjectTag>
    {
        public ProjectTagFacade(TagRepository<ProjectTag> repository) : base(repository)
        {
        }
    }
}
