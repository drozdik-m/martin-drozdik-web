using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using Bonsai.Utils.JSON;

namespace Bonsai.RazorPages.Admin.Services.Traits
{
    interface IOrderableServiceTrait<TEntity, TKey> : IOrderableService<TKey>
        where TEntity : class, IIdentifiable<TKey>, IOrderable
    {
        protected abstract string ApiUri { get; }

        protected abstract HttpClient Http { get; }

        public new async Task ReorderAsync(IEnumerable<TKey> newOrder)
        {
            var response = await Http.PostAsync(ApiUri + "reorder", newOrder.ToJsonContent());
            response.EnsureSuccessStatusCode();
        }
    }
}
