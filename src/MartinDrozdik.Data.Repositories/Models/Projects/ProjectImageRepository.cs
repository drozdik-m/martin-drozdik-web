using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.Repositories.Models.Media.Images;
using MartinDrozdik.Data.Models.Projects;

namespace MartinDrozdik.Data.Repositories.Models.Projects
{
    public abstract class ProjectImageRepository<TMedia> : ImageRepository<TMedia>
        where TMedia : ProjectImage
    {
        public ProjectImageRepository(AppDb context)
            : base(context)
        {

        }
    }
}
