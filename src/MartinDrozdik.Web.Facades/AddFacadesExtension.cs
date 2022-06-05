using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Web.Facades.Models.Blog;
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
        public static IServiceCollection AddIdentityFacades(this IServiceCollection serviceCollection)
        {
            serviceCollection = serviceCollection.AddScoped<UserFacade>();

            return serviceCollection;
        }

        public static IServiceCollection AddFacades(this IServiceCollection serviceCollection)
        {
            serviceCollection = serviceCollection.AddScoped<ProjectTagFacade>();
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
            serviceCollection = serviceCollection.AddScoped<EducationFacade>();
            serviceCollection = serviceCollection.AddScoped<LanguageSkillFacade>();
            serviceCollection = serviceCollection.AddScoped<WorkExperienceFacade>();
            serviceCollection = serviceCollection.AddScoped<ArticleMainImageFacade>();
            serviceCollection = serviceCollection.AddScoped<ArticleTagFacade>();
            serviceCollection = serviceCollection.AddScoped<BlogMarkdownArticleFacade>();
            serviceCollection = serviceCollection.AddScoped<ArticleFacade>();


            return serviceCollection;
        }
    }
}
