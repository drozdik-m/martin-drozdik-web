using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Pages.Error.Services.LanguageDictionary;
using MartinDrozdik.Pages.Error.Services.LanguageDictionary.Languages;
using Bonsai.Services.LanguageDictionary.Abstraction;
using Bonsai.Services.LanguageDictionary.Culture;
using MartinDrozdik.Web.Language.Dictionaries;
using Microsoft.Extensions.DependencyInjection;

namespace Bonsai.Server.Middlewares.Localization
{
    public static class LanguageServicesExtensions
    {
        public static IServiceCollection AddLanguages(this IServiceCollection services)
        {
            //Handle ICultureProvider
            var cultureService = new CultureProvider()
            {
                Culture = CultureInfo.GetCultureInfo("cs")
            };
            services = services.AddSingleton<ICultureProvider>(cultureService);

            //Handle ILanguageDictionary
            var dictionary = new CzechLanguageDictionary();
            services = services.AddSingleton<ILanguageDictionary>(dictionary);

            //Add error language dictionary
            services.AddSingleton<IErrorLanguageDictionary, CzechErrorLanguageDictionary>();

            return services;
        }
    }
}
