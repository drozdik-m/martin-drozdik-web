using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Components.CListPage;
using MartinDrozdik.Web.Admin.Client.Components.CModelCollectionInput;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace MartinDrozdik.Web.Admin.Client.Components.CModelInput
{
    public partial class ModelInput<TModel, TKey> : RootComponent
    {
        [Inject]
        ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Items to select
        /// </summary>
        [Parameter]
        public TModel Value { get; set; }

        /// <summary>
        /// Item changed callback
        /// </summary>
        [Parameter]
        public EventCallback<TModel> ValueChanged { get; set; }

        /// <summary>
        /// Tells if any loading is in progress
        /// </summary>
        protected bool AnyLoading => optionsLoading;

        /// <summary>
        /// Tells if this component should be read only
        /// </summary>
        [Parameter]
        public bool ReadOnly { get; set; } = false;

        /// <summary>
        /// Provides an ID for every model
        /// </summary>
        [Parameter, EditorRequired]
        public Func<TModel, TKey> IdGetter { get; set; }

        /// <summary>
        /// Label of the input
        /// </summary>
        [Parameter]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// CSS class of this input
        /// </summary>
        [Parameter]
        public string Class { get; set; } = string.Empty;

        
        #region Error
        /// <summary>
        /// The last exception that occured
        /// </summary>
        Exception? lastException = default;
        #endregion

        #region Get options
        bool optionsLoading = false;
        bool optionsLoaded = false;

        /// <summary>
        /// A service for getting all the options
        /// </summary>
        [Parameter]
        public IGetAllService<TModel> GetOptionsService { get; set; }

        /// <summary>
        /// List of loaded entities to add to the collection
        /// </summary>
        protected List<TModel> Options { get; set; } = new List<TModel>();

        /// <summary>
        /// Reloads all entities using the getter service as options to add
        /// </summary>
        /// <returns></returns>
        public async Task ReloadOptionsAsync()
        {
            try
            {
                optionsLoading = true;
                Options = (await GetOptionsService.GetAsync()).ToList();
                optionsLoading = false;
                optionsLoaded = true;

                StateHasChanged();
                lastException = null;
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
            }
            finally
            {
                optionsLoading = false;
            }
        }

        #endregion

        #region Default
        /// <summary>
        /// Tells if the value can be cleared to a default value
        /// </summary>
        [Parameter]
        public bool Defaultable { get; set; } = true;

        /// <summary>
        /// The display value of the default value
        /// </summary>
        [Parameter]
        public RenderFragment Default { get; set; }

        /// <summary>
        /// Sets the default value
        /// </summary>
        /// <returns></returns>
        public async Task SetDefaultAsync()
        {
            Value = default;
            await ValueChanged.InvokeAsync(default);
        }
        #endregion
    }
}
