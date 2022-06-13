using System;
using System.Collections.Generic;
using System.Text;
using Bonsai.Models.Abstraction;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services.CRUD;

namespace MartinDrozdik.Web.Facades.Abstraction
{
    public interface ICRUDFacade<TEntity, TKey> : ICRUDService<TEntity, TKey>
        where TEntity : IIdentifiable<TKey>
    {

    }
}
