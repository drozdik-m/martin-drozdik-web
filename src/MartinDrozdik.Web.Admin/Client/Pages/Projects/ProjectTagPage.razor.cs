using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MartinDrozdik.Web.Admin.Client.Pages.Projects
{
    [Authorize(Roles = UserRoles.Developer + "," + UserRoles.Admin)]
    public partial class ProjectTagPage: RootPage
    {
        [Inject]
        ProjectTagService ProjectTagService { get; set; }

        [Inject]
        ISnackbar Snackbar { get; set; }

        public static BreadcrumbItem BreadcrumbItem { get; }
            = new BreadcrumbItem("Project tags", href: "/project-tag", icon: Icons.Material.Filled.Tag);

        protected List<BreadcrumbItem> breadcrumbItems = new()
        {
            Index.BreadcrumbItem,
            BreadcrumbItem,
        };

        protected override async Task OnInitializedAsync()
        {
            await ReloadAsync();
            await base.OnInitializedAsync();
        }

        #region Error
        Exception? lastException = default;
        #endregion

        #region Get
        bool reloadLoading = false;
        
        IGetAllService<ProjectTag> GetService => ProjectTagService;
        List<ProjectTag> Entities { get; set; } = new List<ProjectTag>();

        public async Task ReloadAsync()
        {
            try
            {
                reloadLoading = true;
                Entities = (await GetService.GetAsync()).ToList();
                reloadLoading = false;

                StateHasChanged();
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
            }
            finally
            {
                reloadLoading = false;
            }
        }

        #endregion

        #region Add
        bool addNewDialogOpen = false;
        bool addFormValid = false;
        bool addLoading = false;
        MudForm addForm;
        ProjectTag addItem;
        IAddService<ProjectTag> AddService => ProjectTagService;
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
                await addForm.Validate();
                if (!addForm.IsValid)
                    return;

                addLoading = true;
                await AddService.AddAsync(addItem);
                //await Task.Delay(4000);
                addLoading = false;

                Snackbar.Add("Item successfuly added", Severity.Success);
                addNewDialogOpen = false;

                await ReloadAsync();
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
                addNewDialogOpen = false;
            }
            finally
            {
                addLoading = false;
            }
        }
        #endregion

        #region Delete
        bool deleteDialogOpen = false;
        bool deleteLoading = false;
        IDeleteByKeyService<int> DeleteService => ProjectTagService;
        int toDelete;
        public Task DeleteAsync(int id)
        {
            toDelete = id;
            deleteDialogOpen = true;
            return Task.CompletedTask;
        }
        protected async Task ConfirmDeleteAsync()
        {
            try
            {
                deleteLoading = true;
                await DeleteService.DeleteAsync(toDelete);
                //await Task.Delay(4000);
                deleteLoading = false;

                Snackbar.Add("Item deleted added", Severity.Success);
                deleteDialogOpen = false;

                await ReloadAsync();
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
                deleteDialogOpen = false;
            }
            finally 
            {
                deleteLoading = false;
            }
        }
        #endregion
    }
}
