using MartinDrozdik.Data.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MartinDrozdik.Web.Admin.Client.Pages.Projects
{
    [Authorize(Roles = UserRoles.Developer + "," + UserRoles.Admin)]
    public partial class ProjectTag: RootPage
    {
        public static BreadcrumbItem BreadcrumbItem { get; }
            = new BreadcrumbItem("Project tags", href: "/project-tag", icon: Icons.Material.Filled.Tag);

        protected List<BreadcrumbItem> breadcrumbItems = new()
        {
            Index.BreadcrumbItem,
            BreadcrumbItem,
        };
    }
}
