using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Components.CListPage;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace MartinDrozdik.Web.Admin.Client.Components.CModelCollectionInput
{
    public partial class ModelCollectionInput<TConnector, TModel, TKey> : RootComponent
    {
        [Inject]
        ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Items to edit
        /// </summary>
        [Parameter]
        public ICollection<TConnector> Items { get; set; }

        /// <summary>
        /// Items changed callback
        /// </summary>
        [Parameter]
        public EventCallback<ICollection<TConnector>> ItemsChanged { get; set; }

        /// <summary>
        /// Fragment for rendering individual items
        /// </summary>
        [Parameter]
        public RenderFragment<TConnector> DisplayItem { get; set; }

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
        /// Only unique items can be in the collection
        /// </summary>
        [Parameter]
        public bool Unique { get; set; } = false;

        /// <summary>
        /// Provides an ID for every model
        /// </summary>
        [Parameter, EditorRequired]
        public Func<TConnector, TKey> IdGetter { get; set; }

        [Parameter]
        public Func<TModel, IEnumerable<TConnector>, bool> IsAlreadySelected { get; set; }

        [Parameter]
        public Func<ICollection<TConnector>, IEnumerable<TConnector>> CustomOrder { get; set; }

        /// <summary>
        /// Property for displaying items
        /// </summary>
        public IEnumerable<TConnector> DisplayItems
        {
            get
            {
                if (OrderExpression is not null)
                    return Items.OrderBy(OrderExpression.Compile());
                else if (CustomOrder is not null)
                    return CustomOrder(Items);
                return Items;
            }
        }

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

        protected TModel? SelectedOption { get; set; }

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

        #region Add

        [Parameter, EditorRequired]
        public Func<TModel, TConnector> ConnectorFactory { get; set; }
             
        protected IEnumerable<TModel> ItemsToAdd
        {
            get
            {
                if (!Unique)
                    return Options;
                return Options.Where(e => !IsAlreadySelected(e, Items));
            }
        }

        /// <summary>
        /// Adds the currently selected item to the Items collection
        /// </summary>
        /// <returns></returns>
        public async Task AddSelectedToCollection()
        {
            try
            {
                if (SelectedOption is null)
                {
                    Snackbar.Add("No item selected", Severity.Warning);
                    return;
                }

                Items.Add(ConnectorFactory(SelectedOption));
                await ItemsChanged.InvokeAsync(Items);

                if (Unique)
                    SelectedOption = default;
            }
            catch (Exception e)
            {
                Snackbar.Add("Something went wrong :(", Severity.Error);
                lastException = e;
            }
        }

        public async Task AddNewToCollection()
        {
            try
            {
                Items.Add(ConnectorFactory(default));
                await ItemsChanged.InvokeAsync(Items);
            }
            catch (Exception e)
            {
                Snackbar.Add("Something went wrong :(", Severity.Error);
                lastException = e;
            }
        }

        #endregion

        #region Remove
        protected async Task RemoveItemFromCollection(TConnector toRemove)
        {
            try
            {
                if (IdGetter is null)
                    throw new ArgumentNullException(nameof(IdGetter));

                /*var toRemove = Items
                    .Where(e => IdGetter.Invoke(e).Equals(id))
                    .Where(e => IdGetter.Invoke(e).Equals(id))
                    .SingleOrDefault();*/
                
                if (toRemove is null)
                    return;

                Items.Remove(toRemove);
                await ItemsChanged.InvokeAsync(Items);
            }
            catch (Exception e)
            {
                Snackbar.Add("Something went wrong :(", Severity.Error);
                lastException = e;
            }
        }
        #endregion

        #region Reorder
        bool reordering = false;
        bool reorderLoading = false;
        MudDropContainer<TConnector> reorderContainer;
        int[] originalOrder = Array.Empty<int>();

        /// <summary>
        /// Section for display of reordered item
        /// </summary>
        [Parameter]
        public RenderFragment<TConnector> ReorderItem { get; set; }

        /// <summary>
        /// Expression that identifies the order property (e => e.OrderIndex)
        /// </summary>
        [Parameter]
        public Expression<Func<TConnector, int>> OrderExpression { get; set; }

        /// <summary>
        /// Method for the order control button
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected async Task OrderControlButton()
        {
            if (OrderExpression is null)
                throw new Exception("OrderExpression is not defined");

            if (reordering)
                await ConfirmReorderingAsync();
            else
                await StartReorderingAsync();
        }

        /// <summary>
        /// Starts the reordering process
        /// </summary>
        /// <returns></returns>
        public Task StartReorderingAsync()
        {
            reordering = true;
            originalOrder = Items
                .Select(e => OrderExpression.Compile().Invoke(e))
                .ToArray();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Ends the reordering process and saves the new order
        /// </summary>
        /// <returns></returns>
        public async Task ConfirmReorderingAsync()
        {
            //Check if the order is the same
            var newOrder = Items.Select(e => OrderExpression.Compile().Invoke(e));
            if (newOrder.SequenceEqual(originalOrder))
            {
                Snackbar.Add("The order of items remains the same", Severity.Info);
                reordering = false;
                return;
            }

            /*try
            {
                reorderLoading = true;
                var orderIds = Items
                    .OrderBy(OrderExpression.Compile())
                    .Select(IdGetter);
                await ReorderService.ReorderAsync(orderIds);
                //await Task.Delay(4000);
                reorderLoading = false;

                Snackbar.Add("Items successfuly reordered", Severity.Success);
                reordering = false;

                lastException = null;
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
            }
            finally
            {
                reorderLoading = false;
            }*/

            reordering = false;
        }

        /// <summary>
        /// Reverts the ordering process
        /// </summary>
        /// <returns></returns>
        public Task RevertReorderAsync()
        {
            reordering = false;
            var i = 0;
            foreach (var item in Items)
            {
                if (item is not IOrderable orderableItem)
                    throw new Exception("Could not convert the item into IOrderable");

                orderableItem.OrderIndex = originalOrder[i];
                i++;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Callback for the ordering component
        /// </summary>
        /// <param name="dropItem"></param>
        protected void ReorderUpdated(MudItemDropInfo<TConnector> dropItem)
        {
            Items.UpdateOrder(dropItem, OrderExpression);
        }
        #endregion

        #region Edit
        protected Dictionary<TConnector, bool> EditingStatusesDefaults { get; set; } = new Dictionary<TConnector, bool>();
        protected Dictionary<TKey, bool> EditingStatusesIds { get; set; } = new Dictionary<TKey, bool>();

        [Parameter]
        public RenderFragment<ModelCollectionInputEditContext<TConnector>> EditItem { get; set; }

        public Task ToggleEdit(TConnector model)
        {
            var id = IdGetter(model);
            if (id.Equals(default))
            {
                if (!EditingStatusesDefaults.ContainsKey(model))
                    EditingStatusesDefaults.Add(model, true);
                else
                    EditingStatusesDefaults[model] = !EditingStatusesDefaults[model];
            }
            else
            {
                if (!EditingStatusesIds.ContainsKey(id))
                    EditingStatusesIds.Add(id, true);
                else
                    EditingStatusesIds[id] = !EditingStatusesIds[id];
            }

            StateHasChanged();
            return Task.CompletedTask;
        }

        public bool IsEditing(TConnector model)
        {
            var id = IdGetter(model);
            if (id.Equals(default))
            {
                if (!EditingStatusesDefaults.ContainsKey(model))
                    return false;
                return EditingStatusesDefaults[model];
            }
            else
            {
                if (!EditingStatusesIds.ContainsKey(id))
                    return false;
                return EditingStatusesIds[id];
            }    
        }

        #endregion
    }
}
