using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Data.Repositories.Models.Projects;
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

            return serviceCollection;
        }
    }
}
