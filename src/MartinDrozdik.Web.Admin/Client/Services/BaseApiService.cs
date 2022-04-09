using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services
{
    public abstract class BaseApiService<TEntity, TKey> : IServiceTrait
        where TEntity : IIdentifiable<TKey>
    {
        /// <summary>
        /// Uri address with no "/" at the end
        /// </summary>
        protected abstract string ApiUri { get; set; }

        /// <summary>
        /// The provided HTTP client
        /// </summary>
        protected HttpClient Http { get; set; }

        public BaseApiService(HttpClient http)
        {
            Http = http;
        }

        #region Service trait
        string IServiceTrait.ApiUri => ApiUri;
        HttpClient IServiceTrait.Http => Http;
        #endregion
    }
}
