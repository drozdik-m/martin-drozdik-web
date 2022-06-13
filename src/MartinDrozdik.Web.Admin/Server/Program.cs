using Bonsai.RazorPages.Error.Services.LanguageDictionary.Languages;
using Bonsai.RazorPages.User.Services.LanguageDictionary;
using MartinDrozdik.Data.DbContexts;
using MartinDrozdik.Data.DbContexts.Seeds;
using MartinDrozdik.Data.DbContexts.Seeds.UserSeeds;
using MartinDrozdik.Data.Models.UserIdentity;
using MartinDrozdik.Data.Repositories;
using MartinDrozdik.Services.FilePathProvider;
using MartinDrozdik.Services.FilePathProvider.Specific;
using MartinDrozdik.Services.ImageSaving.Extensions;
using MartinDrozdik.Web.Admin.Server.Configuration;
using MartinDrozdik.Web.Facades;
using MartinDrozdik.Web.Facades.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuration file
var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.Secrets.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

//MVC service
builder.Services
    .AddMvc()
    .AddNewtonsoftJson(e =>
    {
        e.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

//Controllers view builder
var controllersViewBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
    controllersViewBuilder.AddRazorRuntimeCompilation();
controllersViewBuilder.AddNewtonsoftJson();

//Razor pages builder
var razorPagesBuilder = builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
    razorPagesBuilder.AddRazorRuntimeCompilation();
razorPagesBuilder.AddNewtonsoftJson();

//HTTPS
builder.Services.AddHttpsRedirection(options => options.HttpsPort = 443);

//Server configuration
var serverConfiguration = configuration.Get<ServerConfiguration>();
builder.Services.AddSingleton<ServerConfiguration>(serverConfiguration);

//Connection string
var connectionString = serverConfiguration.ConnectionStrings.Production;

//Database context
builder.Services.AddDbContext<AppDb>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<IdentityDb>(options => options.UseSqlServer(connectionString));

//Exceptions filter
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Identity setup
builder.Services
    .AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDb>()
    .AddDefaultTokenProviders();
//.AddDefaultIdentity<AppUser>()


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/user/login";
    options.AccessDeniedPath = "/user/denied";
    options.Cookie.SameSite = SameSiteMode.Strict;
});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = true;
});

//Add authentication
builder.Services.AddAuthentication();

//Add controller views and razor pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Setup seeds
builder.Services.AddSingleton(serverConfiguration.SeedUsers);
builder.Services.AddScoped<UserSeed>();

//Set file path provider
builder.Services.AddSingleton<IFilePathProvider, RegularFilePathProvider>();

//Provide user language
builder.Services.AddSingleton<IUserLanguageDictionary, EnglishUserLanguageDictionary>();

//Add image saver
builder.Services.AddImageSaver();

//Repositories
builder.Services.AddRepositories();

//Seeds
builder.Services.AddSeeds();

//Facades
builder.Services.AddFacades();
builder.Services.AddIdentityFacades();

//Create the app
var app = builder.Build();

//Seed
var seedLoader = new SeedLoader(app);
await seedLoader.LoadSeed<UserSeed>();

// --- Request pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
