using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Data.Repositories.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Technologies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MartinDrozdik.Data.Repositories
{
    public static class AddRepositoriesExtension
    {

        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection = serviceCollection.AddScoped<ProjectTagRepository>();
            serviceCollection = serviceCollection.AddScoped<PersonProfileImageRepository>();
            serviceCollection = serviceCollection.AddScoped<PersonRepository>();
            serviceCollection = serviceCollection.AddScoped<TechnologyRepository>();
            serviceCollection = serviceCollection.AddScoped<TechnologyLogoRepository>();
            serviceCollection = serviceCollection.AddScoped<ProjectRepository>();
            serviceCollection = serviceCollection.AddScoped<ProjectLogoRepository>();
            serviceCollection = serviceCollection.AddScoped<ProjectOgImageRepository>();
            serviceCollection = serviceCollection.AddScoped<ProjectDeveloperRepository>();
            serviceCollection = serviceCollection.AddScoped<ProjectPreviewImageRepository>();
            


            return serviceCollection;
        }
    }
}
