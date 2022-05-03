using MartinDrozdik.Data.Models.Files;
using MartinDrozdik.Data.Models.Markdown.Media;

namespace MartinDrozdik.Web.Admin.Client.Models.Services
{
    public interface IMarkdownArticleService<TArticle, TKey>
    {
        /// <summary>
        /// Uploads a file for an article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<AddFileResponse> UploadFileAsync(TKey id, UploadFileData file);

        /// <summary>
        /// Uploads an image for an article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<AddMediaResponse> UploadRegularImageAsync(TKey id, UploadFileData file);

        /// <summary>
        /// Uploads a text-width image for an article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<AddMediaResponse> UploadTextWidthImageAsync(TKey id, UploadFileData file);

        /// <summary>
        /// Uploads a wide image for an article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<AddMediaResponse> UploadWideImageAsync(TKey id, UploadFileData file);
    }
}
