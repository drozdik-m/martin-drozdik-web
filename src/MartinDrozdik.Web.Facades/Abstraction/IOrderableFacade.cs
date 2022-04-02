using System;
using System.Collections.Generic;
using System.Text;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;

namespace MartinDrozdik.Web.Facades.Abstraction
{
    public interface IOrderableFacade<TEntity, TKey> : IOrderableService<TKey>
        where TEntity : IIdentifiable<TKey>
    {

    }
}
