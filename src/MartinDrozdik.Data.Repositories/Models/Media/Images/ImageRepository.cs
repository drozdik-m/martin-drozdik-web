using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using Bonsai.Models.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Localization;
using MartinDrozdik.Data.Repositories;
using MartinDrozdik.Data.Models.Tags;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Traits;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;

namespace Bonsai.DataPersistence.Repositories.Blog
{
    public abstract class ImageRepository<TMedia> : CRUDRepository<TMedia, int, AppDb>
        where TMedia : Image
    {
        public ImageRepository(AppDb context)
            : base(context)
        {

        }
    }
}
