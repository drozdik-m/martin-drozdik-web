using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Files;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Models.Services;
using MartinDrozdik.Web.Admin.Client.Pages;
using MartinDrozdik.Web.Admin.Client.Pages.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Models.Media.Images;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using MudBlazor.Utilities;

namespace MartinDrozdik.Web.Admin.Client.Components.CImageEditor
{
    public partial class ImageEditor<TImage, TKey> : RootComponent
    {
        [Inject]
        ISnackbar Snackbar { get; set; }

        /// <summary>
        /// The part with inputs and other tools to edit the entity
        /// </summary>
        [Parameter]
        public RenderFragment<ImageEditorInputsContext<TImage>> ExtraInputs { get; set; }

        /// <summary>
        /// How to display the image
        /// </summary>
        [Parameter, EditorRequired]
        public RenderFragment<TImage> ImageDisplay { get; set; }

        /// <summary>
        /// Delete service for the image
        /// </summary>
        [Parameter]
        public IDeleteMediaService<TKey> DeleteService { get; set; }

        /// <summary>
        /// Tells of the inputs of this components are disabled
        /// </summary>
        [Parameter]
        public bool ReadOnly { get; set; } = false;

        /// <summary>
        /// The image
        /// </summary>
        [Parameter]
        public TImage Image { get; set; }

        /// <summary>
        /// Image change callback
        /// </summary>
        [Parameter]
        public EventCallback<TImage> ImageChanged { get; set; }

        /// <summary>
        /// Image Id
        /// </summary>
        [Parameter, EditorRequired]
        public TKey Id { get; set; }

        /// <summary>
        /// Tells if anything is loading
        /// </summary>
        protected bool AnyLoading => uploadLoading || deleteLoading;

        /// <summary>
        /// Cast image into the Image object
        /// </summary>
        protected Image ImageCast
        {
            get
            {
                if (Image is not Image res)
                    throw new Exception("Image could not be cast to Image object");

                return res;
            }
        }

        #region Error
        /// <summary>
        /// The last error the occurred
        /// </summary>
        Exception? lastException = default;
        #endregion

        #region Update
        /// <summary>
        /// The service for updating the image
        /// </summary>
        [Parameter]
        public IUpdateService<TImage, TKey> UpdateService { get; set; }

        /// <summary>
        /// The service for getting the image
        /// </summary>
        [Parameter]
        public IGetByKeyService<TImage, TKey> GetService { get; set; }
        #endregion

        #region AddMedia

        protected UploadFileData? selectedFile = null;
        protected bool uploadLoading = false;

        /// <summary>
        /// Add service for the image
        /// </summary>
        [Parameter]
        public IAddMediaService<TKey> AddService { get; set; }

        /// <summary>
        /// Callback for file selection. Uploads the file.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected async Task UploadMediaAsync(InputFileChangeEventArgs e)
        {
            try
            {
                var file = e.GetMultipleFiles().FirstOrDefault();
                if (file is null)
                    return;

                uploadLoading = true;
                StateHasChanged();

                //Create he upload file data
                var fileData = new UploadFileData();
                var buffers = new byte[file.Size];
                await file
                    .OpenReadStream(maxAllowedSize: 40960000) // max 40 MB
                    .ReadAsync(buffers); 
                fileData.FileName = file.Name;
                fileData.FileSize = file.Size;
                fileData.FileType = file.ContentType;
                fileData.Bytes = buffers;
                selectedFile = fileData;

                //First, save the current model
                await UpdateService.UpdateAsync(Id, Image);

                //Do the upload
                await AddService.AddMediaAsync(Id, selectedFile);

                //Reload the image
                Image = await GetService.GetAsync(Id);
                await ImageChanged.InvokeAsync(Image);

                uploadLoading = false;

                StateHasChanged();

                Snackbar.Add("Image uploaded successfuly", Severity.Success);

                lastException = null;
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong when uploading :(", Severity.Error);
            }
            finally
            {
                uploadLoading = false;
            }
        }
        #endregion

        #region DeleteMedia
        protected bool deleteLoading = false;

        /// <summary>
        /// Deletes the media
        /// </summary>
        /// <returns></returns>
        protected async Task DeleteMediaAsync()
        {
            try
            {
                deleteLoading = true;
                
                //First, save the current model
                await UpdateService.UpdateAsync(Id, Image);

                //Do the deletion
                await DeleteService.DeleteMediaAsync(Id);

                //Reload the image
                Image = await GetService.GetAsync(Id);
                await ImageChanged.InvokeAsync(Image);

                deleteLoading = false;

                StateHasChanged();

                Snackbar.Add("Image deleted successfuly", Severity.Success);

                lastException = null;
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong when deleting :(", Severity.Error);
            }
            finally
            {
                deleteLoading = false;
            }
        }
        #endregion

    }
}
