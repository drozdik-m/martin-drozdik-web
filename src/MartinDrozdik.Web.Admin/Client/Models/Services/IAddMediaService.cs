using MartinDrozdik.Data.Models.Files;

namespace MartinDrozdik.Web.Admin.Client.Models.Services
{
    public interface IAddMediaService<TKey>
    {
        /// <summary>
        /// Adds a physical media to a media item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        Task AddMediaAsync(TKey id, UploadFileData image);
    }
}
