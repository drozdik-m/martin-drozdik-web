using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Web.Facades.Models.Projects;
using MartinDrozdik.Web.Facades.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MartinDrozdik.Web.Facades
{
    public static class AddFacadesExtension
    {
        public static IServiceCollection AddFacades(this IServiceCollection serviceCollection)
        {
            serviceCollection = serviceCollection.AddScoped<ProjectTagFacade>();
            serviceCollection = serviceCollection.AddScoped<UserFacade>();
            serviceCollection = serviceCollection.AddScoped<PersonProfileImageFacade>();

            return serviceCollection;
        }
    }
}
