using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services.CRUD;
using Newtonsoft.Json;

namespace Bonsai.RazorPages.Admin.Services
{
    public abstract class BaseApiService<TEntity, TKey>
        where TEntity : IIdentifiable<TKey>
    {
        /// <summary>
        /// Uri address with no "/" at the end
        /// </summary>
        protected abstract string ApiUri { get; set; }

        protected HttpClient Http { get; set; }

        public BaseApiService(HttpClient http)
        {
            Http = http;
        }
    }
}
