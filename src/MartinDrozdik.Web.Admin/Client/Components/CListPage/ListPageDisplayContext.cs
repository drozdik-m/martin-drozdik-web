namespace MartinDrozdik.Web.Admin.Client.Components.CListPage
{
    public class ListPageDisplayContext<TModel>
    {
        /// <summary>
        /// List of models to display
        /// </summary>
        public IEnumerable<TModel> Entities { get; set; } = new List<TModel>();


        /// <summary>
        /// Tells if loading is in progress
        /// </summary>
        public bool Loading { get; set; }   


        /// <summary>
        /// Tells if a skeleton should be displayed
        /// </summary>
        public bool DisplaySkeleton => Loading && !Entities.Any();

    }
}
