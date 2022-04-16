using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Web.Admin.Client.Services.Authentication;
using MartinDrozdik.Web.Admin.Client.Services.Models.People;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace MartinDrozdik.Web.Admin.Client.Services
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddClientServices(this IServiceCollection serviceCollection)
        {
            //Add auth provider
            serviceCollection = serviceCollection.AddAuthorizationCore();
            serviceCollection = serviceCollection.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

            //Add model services
            serviceCollection = serviceCollection.AddScoped<ProjectTagService>();
            serviceCollection = serviceCollection.AddScoped<PersonService>();
            serviceCollection = serviceCollection.AddScoped<PersonProfileImageService>();
            serviceCollection = serviceCollection.AddScoped<Technology>();
            serviceCollection = serviceCollection.AddScoped<TechnologyLogo>();

            return serviceCollection;
        }
    }
}
