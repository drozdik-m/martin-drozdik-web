using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Repositories.Models.Media.Images;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.DbContexts;
using MartinDrozdik.Data.Models.Blog;

namespace MartinDrozdik.Data.Repositories.Models.Projects;

public abstract class ArticleImageRepository<TMedia> : ImageRepository<TMedia>
    where TMedia : ArticleImage
{
    public ArticleImageRepository(AppDb context)
        : base(context)
    {

    }
}
