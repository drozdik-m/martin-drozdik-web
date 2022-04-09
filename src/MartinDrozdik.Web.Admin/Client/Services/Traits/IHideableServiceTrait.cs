using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using Bonsai.Utils.JSON;

namespace Bonsai.RazorPages.Admin.Services.Traits
{
    interface IHideableServiceTrait<TEntity, TKey> : IHideableService<TEntity>
        where TEntity : class, IIdentifiable<TKey>, IHideable
    {

        protected abstract string ApiUri { get; }

        protected abstract HttpClient Http { get; }

        public new async Task HideAsync(TEntity itemToHide)
        {
            var response = await Http.PutAsync($"{ApiUri}/{itemToHide.Id}/visibility", true.ToJsonContent());
            response.EnsureSuccessStatusCode();

            itemToHide.IsHidden = true;
        }

        public new async Task ShowAsync(TEntity itemToShow)
        {
            var response = await Http.PutAsync($"{ApiUri}/{itemToShow.Id}/visibility", false.ToJsonContent());
            response.EnsureSuccessStatusCode();

            itemToShow.IsHidden = false;
        }

        public new async Task ToggleVisibilityAsync(TEntity itemToToggle)
        {
            var response = await Http.PutAsync($"{ApiUri}/{itemToToggle.Id}/visibility", (!itemToToggle.IsHidden).ToJsonContent());
            response.EnsureSuccessStatusCode();

            itemToToggle.IsHidden = !itemToToggle.IsHidden;
        }
    }
}
