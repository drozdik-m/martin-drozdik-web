using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Services;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;
using MartinDrozdik.Web.Facades.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.Server.Controllers.BaseControllers.Traits
{
    interface IHideableControllerTrait<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>, IHideable
    {
        protected abstract IHideableFacade<TEntity, TKey> HideableFacade { get; }

        public async Task THideAsync(TKey itemToHide)
        {
            try
            {
                await HideableFacade.HideAsync(itemToHide);
            }
            catch (NotFoundException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch(Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }

        public async Task TShowAsync(TKey itemToHide)
        {
            try
            {
                await HideableFacade.ShowAsync(itemToHide);
            }
            catch (NotFoundException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public async Task TToggleVisibilityAsync(TKey itemToToggle)
        {
            try
            {
                await HideableFacade.ToggleVisibilityAsync(itemToToggle);
            }
            catch (NotFoundException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            } 
        }

        public Task TUpdateVisibilityAsync(TKey itemToUpdate, bool newVisibility)
        {
            if (newVisibility)
                return THideAsync(itemToUpdate);
            return TShowAsync(itemToUpdate);
        }
    }
}
