using System;
using System.Collections.Generic;
using System.Text;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services.CRUD;

namespace MartinDrozdik.Web.Facades.Abstraction
{
    public interface ICRUDFacade<TEntity, TKey> : ICRUDService<TEntity, TKey>
        where TEntity : IIdentifiable<TKey>
    {

    }
}
