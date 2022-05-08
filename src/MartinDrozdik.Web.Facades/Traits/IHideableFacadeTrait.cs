using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Services;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Traits.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Web.Facades.Traits
{
    interface IHideableFacadeTrait<TEntity, TKey>
        : IHideableFacade<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>, IHideable
    {
        protected abstract IHideableRepository<TEntity, TKey> HideableRepository { get; }

        public Task<IEnumerable<TEntity>> TGetVisibleAsync() => HideableRepository.GetVisibleAsync();

        public Task THideAsync(TKey itemToHide) => HideableRepository.HideAsync(itemToHide);

        public Task TShowAsync(TKey itemToHide) => HideableRepository.ShowAsync(itemToHide);

        public Task TToggleVisibilityAsync(TKey itemToToggle) => HideableRepository.ToggleVisibilityAsync(itemToToggle);
    }
}
