using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Utils.JSON;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services;

namespace MartinDrozdik.Web.Admin.Client.Services.Traits
{
    interface IHideableServiceTrait<TEntity, TKey> : IServiceTrait, IHideableService<TEntity>
        where TEntity : class, IIdentifiable<TKey>, IHideable
    {
        public async Task THideAsync(TEntity itemToHide)
        {
            var response = await Http.PutAsync($"{ApiUri}/{itemToHide.Id}/visibility", true.ToJsonContent());
            response.EnsureSuccessStatusCode();

            itemToHide.IsHidden = true;
        }

        public async Task TShowAsync(TEntity itemToShow)
        {
            var response = await Http.PutAsync($"{ApiUri}/{itemToShow.Id}/visibility", false.ToJsonContent());
            response.EnsureSuccessStatusCode();

            itemToShow.IsHidden = false;
        }

        public async Task TToggleVisibilityAsync(TEntity itemToToggle)
        {
            var response = await Http.PutAsync($"{ApiUri}/{itemToToggle.Id}/visibility", (!itemToToggle.IsHidden).ToJsonContent());
            response.EnsureSuccessStatusCode();

            itemToToggle.IsHidden = !itemToToggle.IsHidden;
        }
    }
}
