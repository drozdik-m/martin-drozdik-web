namespace MartinDrozdik.Web.Admin.Client.Components.CModelCollectionInput
{
    public class ModelCollectionInputEditContext<TModel>
    {
        /// <summary>
        /// The model that is edited
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// True if inputs should be read only
        /// </summary>
        public bool ReadOnly { get; set; } = false;
    }
}
