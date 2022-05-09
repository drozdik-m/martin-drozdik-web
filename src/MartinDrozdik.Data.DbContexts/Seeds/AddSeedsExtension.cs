using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MartinDrozdik.Data.DbContexts.Seeds.CVSeeds;
using MartinDrozdik.Data.DbContexts.Seeds.UserSeeds;

namespace MartinDrozdik.Data.DbContexts.Seeds
{
    public static class AddSeedsExtension
    {
        public static IServiceCollection AddSeeds(this IServiceCollection serviceCollection)
        {
            //serviceCollection = serviceCollection.AddScoped<UserSeed>();
            serviceCollection = serviceCollection.AddScoped<EducationSeed>();
            serviceCollection = serviceCollection.AddScoped<LanguageSkillSeed>();
            serviceCollection = serviceCollection.AddScoped<WorkExperienceSeed>();

            return serviceCollection;
        }
    }
}
