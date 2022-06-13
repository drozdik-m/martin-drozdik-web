using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Services;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Web.Facades.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.Server.Controllers.BaseControllers.Traits
{
    interface IOrderableControllerTrait<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>, IOrderable
    {
        protected abstract IOrderableFacade<TEntity, TKey> OrderableFacade { get; }

        public async Task TReorderAsync(IEnumerable<TKey> newOrder)
        {
            try
            {
                await OrderableFacade.ReorderAsync(newOrder);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
