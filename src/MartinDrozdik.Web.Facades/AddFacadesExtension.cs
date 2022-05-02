using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Projects;
using MartinDrozdik.Web.Facades.Models.Technologies;
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
            serviceCollection = serviceCollection.AddScoped<PersonFacade>();
            serviceCollection = serviceCollection.AddScoped<TechnologyFacade>();
            serviceCollection = serviceCollection.AddScoped<TechnologyLogoFacade>();
            serviceCollection = serviceCollection.AddScoped<ProjectFacade>();
            serviceCollection = serviceCollection.AddScoped<ProjectLogoFacade>();
            serviceCollection = serviceCollection.AddScoped<ProjectOgImageFacade>();
            serviceCollection = serviceCollection.AddScoped<ProjectDeveloperFacade>();
            serviceCollection = serviceCollection.AddScoped<ProjectGalleryImageFacade>();
            serviceCollection = serviceCollection.AddScoped<ProjectPreviewImageFacade>();
            serviceCollection = serviceCollection.AddScoped<ProjectMarkdownArticleFacade>();


            return serviceCollection;
        }
    }
}
