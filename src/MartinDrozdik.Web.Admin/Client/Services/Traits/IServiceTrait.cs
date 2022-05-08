using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Utils.JSON;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Traits
{
    interface IServiceTrait
    {
        protected abstract string ApiUri { get; }

        protected abstract HttpClient Http { get; }
    }
}
