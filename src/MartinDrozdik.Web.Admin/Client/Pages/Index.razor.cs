using MartinDrozdik.Data.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using MudBlazor;

namespace MartinDrozdik.Web.Admin.Client.Pages
{
    [Authorize(Roles = UserRoles.Developer + "," + UserRoles.Admin)]
    public partial class Index : RootPage
    {
        public static BreadcrumbItem BreadcrumbItem { get; }
            = new BreadcrumbItem("Admin", href: "/", icon: Icons.Material.Filled.Home);
    }
}
