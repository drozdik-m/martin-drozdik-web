namespace MartinDrozdik.Web.Admin.Client.Components.CImageEditor
{
    public class ImageEditorInputsContext<TModel>
    {
        /// <summary>
        /// The image model that is edited
        /// </summary>
        public TModel Image { get; set; }

        /// <summary>
        /// True if inputs should be read only / disabled
        /// </summary>
        public bool ReadOnly { get; set; } = false;
    }
}
