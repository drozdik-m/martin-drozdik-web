using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Web.Facades.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Web.Facades.Traits.Abstraction
{
    interface ICRUDFacadeTrait<TEntity, TKey> : ICRUDFacade<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>
    {
        protected ICRUDRepository<TEntity, TKey> CRUDRepository { get; }
    }
}
