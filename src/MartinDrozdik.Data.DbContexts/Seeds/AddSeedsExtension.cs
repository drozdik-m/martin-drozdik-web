using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.DbContexts.Seeds.CVSeed;
using MartinDrozdik.Data.DbContexts.Seeds.UserSeed;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MartinDrozdik.Web.Facades
{
    public static class AddSeedsExtension
    {
        public static IServiceCollection AddSeeds(this IServiceCollection serviceCollection)
        {
            serviceCollection = serviceCollection.AddScoped<UserSeed>();
            serviceCollection = serviceCollection.AddScoped<EducationSeed>();
            serviceCollection = serviceCollection.AddScoped<LanguageSkillSeed>();
            serviceCollection = serviceCollection.AddScoped<WorkExperienceSeed>();

            return serviceCollection;
        }
    }
}
