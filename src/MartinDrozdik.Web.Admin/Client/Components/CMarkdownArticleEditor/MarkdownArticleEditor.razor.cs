using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Files;
using MartinDrozdik.Data.Models.Markdown;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Components.CImageEditor;
using MartinDrozdik.Web.Admin.Client.Models.Services;
using MartinDrozdik.Web.Admin.Client.Pages;
using MartinDrozdik.Web.Admin.Client.Pages.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Models.Media;
using MartinDrozdik.Web.Admin.Client.Services.Models.Media.Images;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using MudBlazor.Utilities;

namespace MartinDrozdik.Web.Admin.Client.Components.CMarkdownArticleEditor
{
    public partial class MarkdownArticleEditor<TArticle, TKey> : RootComponent
    {
        [Inject]
        ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Service for the article
        /// </summary>
        [Parameter]
        public IMarkdownArticleService<TArticle, TKey> ArticleService { get; set; }

        /// <summary>
        /// Tells of the inputs of this components are disabled
        /// </summary>
        [Parameter]
        public bool ReadOnly { get; set; } = false;

        /// <summary>
        /// The article
        /// </summary>
        [Parameter, EditorRequired]
        public TArticle Article { get; set; }

        /// <summary>
        /// Article change callback
        /// </summary>
        [Parameter]
        public EventCallback<TArticle> ArticleChanged { get; set; }

        /// <summary>
        /// Article Id
        /// </summary>
        [Parameter, EditorRequired]
        public TKey Id { get; set; }

        /// <summary>
        /// Tells if anything is loading
        /// </summary>
        protected bool AnyLoading => uploadLoading;

        /// <summary>
        /// How many lines the input should have
        /// </summary>
        [Parameter]
        public int LineCount { get; set; } = 35;

        /// <summary>
        /// Cast image into the Image object
        /// </summary>
        protected MarkdownArticle ArticleCast
        {
            get
            {
                if (Article is not MarkdownArticle res)
                    throw new Exception("Article could not be cast to MarkdownArticle object");

                return res;
            }
        }

        #region Error
        /// <summary>
        /// The last error the occurred
        /// </summary>
        Exception? lastException = default;
        #endregion

        #region UploadMedia

        //protected UploadFileData? selectedFile = null;
        protected bool uploadLoading = false;


        protected Task UploadFileAsync(InputFileChangeEventArgs e)
            => UploadMediaAsync(e, async f =>
            {
                var res = await ArticleService.UploadFileAsync(Id, f);
                ArticleCast.Markdown += "\n";
                ArticleCast.Markdown += $"[{f.FileName}]({res.MediaURI})" + "{.file}\n";
            });

        protected Task UploadRegularImageAsync(InputFileChangeEventArgs e)
            => UploadMediaAsync(e, async f =>
            {
                var res = await ArticleService.UploadRegularImageAsync(Id, f);
                ArticleCast.Markdown += "\n";
                ArticleCast.Markdown += "^^^\n";
                ArticleCast.Markdown += $"![{f.FileName}]({res.MediaURI})\n";
                ArticleCast.Markdown += $"^^^ Image: {f.FileName}\n";
            });

        protected Task UploadTextWidthImageAsync(InputFileChangeEventArgs e)
            => UploadMediaAsync(e, async f =>
            {
                var res = await ArticleService.UploadRegularImageAsync(Id, f);
                ArticleCast.Markdown += "\n";
                ArticleCast.Markdown += "^^^\n";
                ArticleCast.Markdown += $"![{f.FileName}]({res.MediaURI})\n";
                ArticleCast.Markdown += $"^^^ Image: {f.FileName}" + "{.textWidth}\n";
            });

        protected Task UploadWideImageAsync(InputFileChangeEventArgs e)
            => UploadMediaAsync(e, async f =>
            {
                var res = await ArticleService.UploadRegularImageAsync(Id, f);
                ArticleCast.Markdown += "\n";
                ArticleCast.Markdown += "^^^\n";
                ArticleCast.Markdown += $"![{f.FileName}]({res.MediaURI})\n";
                ArticleCast.Markdown += $"^^^ Image: {f.FileName}" + "{.wide}\n";
            });

        /// <summary>
        /// Uploads the file
        /// </summary>
        /// <param name="e"></param>
        /// <param name="upload"></param>
        /// <returns></returns>
        protected async Task UploadMediaAsync(InputFileChangeEventArgs e, Func<UploadFileData, Task> upload)
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

                //Do the upload
                await upload.Invoke(fileData);

                //Notify change
                await ArticleChanged.InvokeAsync(Article);

                uploadLoading = false;
                StateHasChanged();

                Snackbar.Add("File uploaded successfuly", Severity.Success);

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

        #region Guide
        bool guideDialogOpen = false;

        public async Task AddText(string text)
        {
            ArticleCast.Markdown += text;
            await ArticleChanged.InvokeAsync(Article);
        }
        #endregion
    }
}
