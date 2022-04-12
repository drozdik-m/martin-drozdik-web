using MudBlazor;

namespace MartinDrozdik.Web.Admin.Client.Pages
{
    public static class PredefinedBreadcrumbs
    {
        public static BreadcrumbItem Loading { get; } 
            = new BreadcrumbItem("Loading...", "/", disabled: true, icon: Icons.Material.Filled.Refresh);
    }
}
