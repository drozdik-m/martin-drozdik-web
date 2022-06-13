namespace MartinDrozdik.Web.Admin.Client.Models.Services
{
    public interface IDeleteMediaService<TKey>
    {
        /// <summary>
        /// Removes physical media from a media entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteMediaAsync(TKey id);
    }
}
