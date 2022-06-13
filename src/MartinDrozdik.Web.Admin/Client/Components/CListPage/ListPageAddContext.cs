namespace MartinDrozdik.Web.Admin.Client.Components.CListPage
{
    public class ListPageAddContext<TModel>
    {
        /// <summary>
        /// The model that is edited
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// True if any "add" loading is in progress
        /// </summary>
        public bool Loading { get; set; } = false;
    }
}
