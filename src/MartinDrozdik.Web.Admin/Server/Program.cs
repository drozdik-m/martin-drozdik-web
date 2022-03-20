using Bonsai.DataPersistence.DbContexts;
using MartinDrozdik.Data.DbContexts.Seeds;
using MartinDrozdik.Data.DbContexts.Seeds.UserSeed;
using MartinDrozdik.Data.Models.UserIdentity;
using MartinDrozdik.Web.Admin.Server.Configuration;
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

//Server configuration
var serverConfiguration = configuration
    .Get<ServerConfiguration>();
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
    .AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityDb>();
/*services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/user/login";
                //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });*/
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
});

//Identity server
builder.Services
    .AddIdentityServer()
    .AddApiAuthorization<AppUser, IdentityDb>();

//Add authentication
//https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-6.0#deploy-to-production
//https://devblogs.microsoft.com/dotnet/asp-net-core-authentication-with-identityserver4/
builder.Services
    .AddAuthentication()
    .AddIdentityServerJwt();

//Add controller views and razor pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Setup seeds
builder.Services.AddSingleton(serverConfiguration.SeedUsers);
builder.Services.AddScoped<UserSeed>();

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

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
