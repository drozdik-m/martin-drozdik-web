namespace MartinDrozdik.Web.Admin.Client.Components.CEditPage
{
    public class EditPageEditContext<TModel>
    {
        /// <summary>
        /// The entity that is edited
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// Are the input supposed to be read only?
        /// </summary>
        public bool Disabled { get; set; }
    }
}
