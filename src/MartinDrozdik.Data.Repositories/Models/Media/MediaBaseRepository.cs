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
using MartinDrozdik.Data.DbContexts;

namespace MartinDrozdik.Data.Repositories.Models.Media
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
