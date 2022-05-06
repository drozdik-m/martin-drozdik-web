using Bonsai.Server.Middlewares.Localization;
using Bonsai.Services.Email.Extensions;
using Bonsai.Services.RecaptchaV2.Configuration;
using Bonsai.Services.RecaptchaV2.Extensions;
using MartinDrozdik.Services.FilePathProvider;
using MartinDrozdik.Services.FilePathProvider.Specific;
using MartinDrozdik.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MartinDrozdik.Data.Models.UserIdentity;
using MartinDrozdik.Data.DbContexts.Seeds.UserSeed;
using MartinDrozdik.Data.DbContexts;

namespace MartinDrozdik.Web
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Environment = environment;
            Configuration = configuration;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.Secrets.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //MVC service
            services.AddMvc().AddNewtonsoftJson();

            //Controllers view builder
            var controllersViewBuilder = services.AddControllersWithViews();
            if (Environment.IsDevelopment())
                controllersViewBuilder.AddRazorRuntimeCompilation();
            controllersViewBuilder.AddNewtonsoftJson();

            //Razor pages builder
            var razorPagesBuilder = services.AddRazorPages();
            if (Environment.IsDevelopment())
                razorPagesBuilder.AddRazorRuntimeCompilation();
            razorPagesBuilder.AddNewtonsoftJson();
            
            //HTTPS
            services.AddHttpsRedirection(options => options.HttpsPort = 443);

            //Response compression
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            //Server configuration
            var serverConfiguration = Configuration
                .Get<ServerConfiguration>();
            services.AddSingleton<ServerConfiguration>(serverConfiguration);

            //Recaptcha validator
            services.AddRecaptchaV2Validator(serverConfiguration.Recaptcha);

            //Email sender
            services.AddEmailSender(serverConfiguration.EmailSender);

            //Connection string
            var connectionString = serverConfiguration.ConnectionStrings.Production;

            //Database context
            services.AddDbContext<AppDb>(options =>
                options.UseSqlServer(connectionString));

            //File path provider service
            if (Environment.IsDevelopment())
                services.AddSingleton<IFilePathProvider, RegularFilePathProvider>();
            else
                services.AddSingleton<IFilePathProvider, VersionedFilePathProvider>();

            //Add languages
            services.AddLanguages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();


            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
                app.UseHsts();
            }

            //Add https redirect, files and auth
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            //Use standard endpoints for MVC and razor pages
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
