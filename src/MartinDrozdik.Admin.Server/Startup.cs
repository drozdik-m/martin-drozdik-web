using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.DbContexts.Seeds.UserSeed;
using MartinDrozdik.Data.Models.UserIdentity;
using MartinDrozdik.Services.FilePathProvider;
using MartinDrozdik.Services.FilePathProvider.Specific;
using MartinDrozdik.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace MartinDrozdik.Admin.Server
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

            //Connection string
            var connectionString = serverConfiguration.ConnectionStrings.Production;

            //Database context
            services.AddDbContext<AppDb>(options =>
                options.UseSqlServer(connectionString));

            //Seeds
            services.AddSingleton(serverConfiguration.SeedUsers);
            services.AddScoped<UserSeed>();

            //Identity setup
            services.AddDbContext<IdentityDb>(options =>
                options.UseSqlServer(connectionString));
            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<IdentityDb>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/user/login";
                //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            });

            //File path provider service
            if (Environment.IsDevelopment())
                services.AddSingleton<IFilePathProvider, RegularFilePathProvider>();
            else
                services.AddSingleton<IFilePathProvider, VersionedFilePathProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
                app.UseHsts();
            }

            //Add https redirect, files and auth
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
