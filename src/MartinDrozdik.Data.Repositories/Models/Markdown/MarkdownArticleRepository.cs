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
using MartinDrozdik.Data.Models.Markdown;
using MartinDrozdik.Data.DbContexts;

namespace MartinDrozdik.Data.Repositories.Models.Media
{
    public abstract class MarkdownArticleRepository<TArticle> : CRUDRepository<TArticle, int, AppDb>
        where TArticle : MarkdownArticle
    {
        public MarkdownArticleRepository(AppDb context)
            : base(context)
        {

        }

        protected override Expression<Func<TArticle, bool>> IdPredicate(int targetId)
            => e => e.Id == targetId;
    }
}
