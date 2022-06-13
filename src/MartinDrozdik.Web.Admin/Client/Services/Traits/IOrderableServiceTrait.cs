using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Utils.JSON;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services;

namespace MartinDrozdik.Web.Admin.Client.Services.Traits
{
    interface IOrderableServiceTrait<TEntity, TKey> : IServiceTrait, IOrderableService<TKey>
        where TEntity : class, IIdentifiable<TKey>, IOrderable
    {
        public async Task TReorderAsync(IEnumerable<TKey> newOrder)
        {
            var response = await Http.PutAsync(ApiUri + "/reorder", newOrder.ToJsonContent());
            response.EnsureSuccessStatusCode();
        }
    }
}
