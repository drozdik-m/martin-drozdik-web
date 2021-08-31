using Bonsai.Server.Middlewares.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Web
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
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

            //Add languages
            services.AddLanguages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            //Exception pages
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseStatusCodePagesWithReExecute("/error", "?code={0}");


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
