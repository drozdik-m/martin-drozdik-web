using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Components;
using MartinDrozdik.Web.Admin.Client.Pages;
using MartinDrozdik.Web.Admin.Client.Pages.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace MartinDrozdik.Web.Admin.Client.Components.CEditPage
{
    public partial class EditPage<TModel, TKey> : RootComponent
    {
        [Inject]
        ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Id of the edited entity
        /// </summary>
        [Parameter, EditorRequired]
        public TKey Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ReloadAsync();

            if (Entity is null)
                return;

            await base.OnInitializedAsync();
        }

        /// <summary>
        /// Indicates that any loading is in progress
        /// </summary>
        protected bool AnyLoading => reloadLoading || editLoading;

        /// <summary>
        /// Tells if the inputs are readonly or not
        /// </summary>
        protected bool ReadOnly => Editor is null || UpdateService is null;

        /// <summary>
        /// Should the inputs be disabled?
        /// </summary>
        protected bool Disabled => ReadOnly || AnyLoading;

        /// <summary>
        /// Callback event on entity load
        /// </summary>
        [Parameter]
        public Action<TModel> OnEntityLoad { get; set; }

        /// <summary>
        /// The part with inputs and other tools to edit the entity
        /// </summary>
        [Parameter, EditorRequired]
        public RenderFragment<EditPageEditContext<TModel>> Editor { get; set; }

        #region Error
        /// <summary>
        /// The last error the occurred
        /// </summary>
        Exception? lastException = default;
        #endregion

        #region Get
        bool reloadLoading = false;

        /// <summary>
        /// The service for getting the item
        /// </summary>
        [Parameter, EditorRequired]
        public IGetByKeyService<TModel, TKey> GetService { get; set; }

        /// <summary>
        /// The current entity
        /// </summary>
        protected TModel Entity { get; set; }

        /// <summary>
        /// Reloads the current entity
        /// </summary>
        /// <param name="verbose">If true, messages about success will be used</param>
        /// <returns></returns>
        public async Task ReloadAsync(bool verbose = false)
        {
            try
            {
                reloadLoading = true;
                Entity = await GetService.GetAsync(Id);
                OnEntityLoad?.Invoke(Entity);
                reloadLoading = false;

                StateHasChanged();

                if (verbose)
                    Snackbar.Add("Item successfuly loaded", Severity.Success);
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

        #region Update
        bool editLoading = false;
        bool editFormValid;
        MudForm editForm;

        /// <summary>
        /// The service for updating the entity
        /// </summary>
        [Parameter]
        public IUpdateService<TModel, TKey> UpdateService { get; set; }

        /// <summary>
        /// Saves the current entity as is
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            try
            {
                await editForm.Validate();
                if (!editForm.IsValid)
                    Snackbar.Add("There are errors in the form", Severity.Warning);

                editLoading = true;
                await UpdateService.UpdateAsync(Id, Entity);
                editLoading = false;

                await ReloadAsync();

                StateHasChanged();

                Snackbar.Add("Changes successfuly saved", Severity.Success);
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
            }
            finally
            {
                editLoading = false;
            }
        }
        #endregion

    }
}
