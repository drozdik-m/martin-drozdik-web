using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Services.CRUD;

namespace MartinDrozdik.Abstraction.Services.CRUD
{
    public interface ICRUDService<TEntity, TKey>
        : IAddService<TEntity>,
        IDeleteByKeyService<TKey>,
        IGetAllService<TEntity>,
        IGetByKeyService<TEntity, TKey>,
        IUpdateService<TEntity, TKey>
    {

    }
}
