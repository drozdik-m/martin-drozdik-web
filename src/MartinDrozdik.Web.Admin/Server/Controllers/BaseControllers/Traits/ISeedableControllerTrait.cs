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
    interface ISeedableControllerTrait
    {
        protected abstract ISeedableFacade SeedableFacade { get; }

        public async Task TSeedAsync()
        {
            try
            {
                await SeedableFacade.SeedAsync();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            } 
        }

    }
}
