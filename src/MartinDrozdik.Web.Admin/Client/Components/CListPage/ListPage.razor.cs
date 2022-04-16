using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace MartinDrozdik.Web.Admin.Client.Components.CListPage
{
    public partial class ListPage<TModel, TKey> : RootComponent
    {
        [Inject]
        ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ReloadAsync();
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// Tells if any loading is in progress
        /// </summary>
        protected bool AnyLoading => addLoading || deleteLoading || reloadLoading || reorderLoading;


        /// <summary>
        /// Getter expression for the key
        /// </summary>
        [Parameter, EditorRequired]
        public Expression<Func<TModel, TKey>> KeyGetter { get; set; }

        [Parameter]
        public Func<List<TModel>, IEnumerable<TModel>> CustomOrder { get; set; }

        /// <summary>
        /// Property for displaying entities
        /// </summary>
        protected IEnumerable<TModel> DisplayEntities
        {
            get
            {
                if (OrderExpression is not null)
                    return Entities.OrderBy(OrderExpression.Compile());
                else if (CustomOrder is not null)
                    return CustomOrder(Entities);
                return Entities;
            }
        }

        #region Error
        /// <summary>
        /// The last exception that occured
        /// </summary>
        Exception? lastException = default;
        #endregion

        #region Get
        bool reloadLoading = false;

        /// <summary>
        /// Fragment to display the items in a table or other
        /// </summary>
        [Parameter]
        public RenderFragment<ListPageDisplayContext<TModel>> DisplayItems { get; set; }

        /// <summary>
        /// A service for getting all the entities
        /// </summary>
        [Parameter, EditorRequired]
        public IGetAllService<TModel> GetService { get; set; }

        /// <summary>
        /// List of loaded entities 
        /// </summary>
        protected List<TModel> Entities { get; set; } = new List<TModel>();

        /// <summary>
        /// Reloads all entities using the getter service
        /// </summary>
        /// <param name="verbose">If true, success notification messages will be used</param>
        /// <returns></returns>
        public async Task ReloadAsync(bool verbose = false)
        {
            try
            {
                reloadLoading = true;
                Entities = (await GetService.GetAsync()).ToList();
                reloadLoading = false;

                StateHasChanged();

                if (verbose)
                    Snackbar.Add("Items successfuly reloaded", Severity.Success);

                lastException = null;
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
        TModel addItem;

        /// <summary>
        /// Section for inputs of added model
        /// </summary>
        [Parameter]
        public RenderFragment<ListPageAddContext<TModel>> AddInputs { get; set; }

        /// <summary>
        /// The means of providing new entity instance for adding
        /// </summary>
        [Parameter]
        public Func<TModel> EntityCreator { get; set; }

        /// <summary>
        /// Service for adding new entities
        /// </summary>
        [Parameter]
        public IAddService<TModel> AddService { get; set; }

        /// <summary>
        /// Starts the process of creating an item
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task AddItemAsync()
        {
            if (EntityCreator is null)
                throw new Exception("EntityCreator is not defined");

            addNewDialogOpen = true;
            addItem = EntityCreator();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Confirms and adds new item
        /// </summary>
        /// <returns></returns>
        public async Task AddItemConfirmAsync()
        {
            try
            {
                await addForm.Validate();
                if (!addForm.IsValid)
                    Snackbar.Add("There are errors in the form", Severity.Warning);

                addLoading = true;
                await AddService.AddAsync(addItem);
                //await Task.Delay(4000);
                addLoading = false;

                Snackbar.Add("Item successfuly added", Severity.Success);
                addNewDialogOpen = false;

                lastException = null;

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
        TKey toDelete;

        /// <summary>
        /// The service for deleting existing entities
        /// </summary>
        [Parameter]
        public IDeleteByKeyService<TKey> DeleteService { get; set; }

        /// <summary>
        /// Starts the deletion process
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAsync(TKey id)
        {
            toDelete = id;
            deleteDialogOpen = true;
            StateHasChanged();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Finishes the deletion process
        /// </summary>
        /// <returns></returns>
        protected async Task ConfirmDeleteAsync()
        {
            try
            {
                deleteLoading = true;
                await DeleteService.DeleteAsync(toDelete);
                //await Task.Delay(4000);
                deleteLoading = false;

                Snackbar.Add("Item deleted", Severity.Success);
                deleteDialogOpen = false;

                lastException = null;

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

        #region Reorder
        bool reordering = false;
        bool reorderLoading = false;
        MudDropContainer<TModel> reorderContainer;
        int[] originalOrder = Array.Empty<int>();

        /// <summary>
        /// Section for display of reordered item
        /// </summary>
        [Parameter]
        public RenderFragment<TModel> ReorderItem { get; set; }

        /// <summary>
        /// Expression that identifies the order property (e => e.OrderIndex)
        /// </summary>
        [Parameter]
        public Expression<Func<TModel, int>> OrderExpression { get; set; }

        /// <summary>
        /// Service for reordering
        /// </summary>
        [Parameter]
        public IOrderableService<TKey> ReorderService { get; set; }

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
            originalOrder = Entities
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
            var newOrder = Entities.Select(e => OrderExpression.Compile().Invoke(e));
            if (newOrder.SequenceEqual(originalOrder))
            {
                Snackbar.Add("The order of items remains the same", Severity.Info);
                reordering = false;
                return;
            }

            try
            {
                reorderLoading = true;
                var orderIds = Entities
                    .OrderBy(OrderExpression.Compile())
                    .Select(KeyGetter.Compile());
                await ReorderService.ReorderAsync(orderIds);
                //await Task.Delay(4000);
                reorderLoading = false;

                Snackbar.Add("Items successfuly reordered", Severity.Success);
                reordering = false;

                lastException = null;

                await ReloadAsync();
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
            }
            finally
            {
                reorderLoading = false;
            }
        }

        /// <summary>
        /// Reverts the ordering process
        /// </summary>
        /// <returns></returns>
        public Task RevertReorderAsync()
        {
            reordering = false;
            var i = 0;
            foreach (var item in Entities)
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
        protected void ReorderUpdated(MudItemDropInfo<TModel> dropItem)
        {
            Entities.UpdateOrder(dropItem, OrderExpression);
        }
        #endregion
    }
}
