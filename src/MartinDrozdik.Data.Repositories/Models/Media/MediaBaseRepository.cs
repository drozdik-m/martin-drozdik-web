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

namespace Bonsai.DataPersistence.Repositories.Blog
{
    public abstract class MediaBaseRepository<TMedia> : CRUDRepository<TMedia, int, AppDb>
        where TMedia : MediaBase
    {
        public MediaBaseRepository(AppDb context)
            : base(context)
        {

        }

        protected override Expression<Func<TMedia, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;
    }
}
