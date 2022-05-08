using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Utils.JSON;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services;

namespace MartinDrozdik.Web.Admin.Client.Services.Traits
{
    interface ISeedableServiceTrait : IServiceTrait, ISeedableService
    {
        /// <inheritdoc/>
        public async Task TSeedAsync()
        {
            var response = await Http.PostAsync(ApiUri + "/seed", "".ToJsonContent());
            response.EnsureSuccessStatusCode();
        }
    }
}
