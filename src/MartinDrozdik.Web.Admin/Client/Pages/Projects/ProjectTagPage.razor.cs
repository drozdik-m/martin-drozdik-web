using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MartinDrozdik.Web.Admin.Client.Pages.Projects
{
    [Authorize(Roles = UserRoles.Developer + "," + UserRoles.Admin)]
    public partial class ProjectTagPage: RootPage
    {
        public static BreadcrumbItem BreadcrumbItem { get; }
            = new BreadcrumbItem("Project tags", href: "/project-tag", icon: Icons.Material.Filled.Tag);

        protected List<BreadcrumbItem> breadcrumbItems = new()
        {
            Index.BreadcrumbItem,
            BreadcrumbItem,
        };

        #region Error
        Exception? lastException = default;
        #endregion

        #region Add
        bool addNewDialogOpen = false;
        ProjectTag addItem; 
        public Task AddItemAsync()
        {
            addNewDialogOpen = true;
            addItem = new ProjectTag();
            return Task.CompletedTask;
        }

        public async Task AddItemConfirmAsync()
        {
            try
            {

            }
            catch (Exception ex)
            {
                lastException = ex;
                addNewDialogOpen = false;
            }
        }
        #endregion
    }
}
