using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Utils.String;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Tags;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Media;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Web.Facades.Models.Media
{
    public class MediaBaseFacade<TMedia> : ContentFacade<TMedia, int>
        where TMedia : MediaBase
    {
        readonly IHostEnvironment hostEnvironment;
        readonly MediaBaseRepository<TMedia> repository;

        public MediaBaseFacade(IHostEnvironment hostEnvironment,
            MediaBaseRepository<TMedia> repository)
            : base(hostEnvironment, repository)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
        }

        /// <summary>
        /// Adds media with the media data
        /// </summary>
        /// <param name="mediaData"></param>
        /// <param name="data"></param>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public virtual async Task AddMediaAsync(TMedia mediaData, Stream data, string dataName)
        {
            //Delete the old media if exists
            DisposeContentFile(mediaData);

            //Ensure the target exists
            mediaData.FileName = dataName.ToUrlFriendlyFileName();
            EnsureTargetFolderExists(mediaData);

            //Update the data model
            mediaData.Uploaded = true;
            await repository.UpdateAsync(mediaData.Id, mediaData);
        }

        /// <summary>
        /// Adds media with the media data
        /// </summary>
        /// <param name="mediaData"></param>
        /// <param name="data"></param>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public virtual async Task AddMediaAsync(TMedia mediaData, IEnumerable<byte> data, string dataName)
        {
            using Stream stream = new MemoryStream(data.ToArray());
            await AddMediaAsync(mediaData, stream, dataName);
        }

        /// <summary>
        /// Deletes the media of a media entity
        /// </summary>
        /// <param name="mediaData"></param>
        /// <returns></returns>
        public virtual async Task DeleteMediaAsync(TMedia mediaData)
        {
            //Delete the physical media
            DisposeContentFile(mediaData);

            //Update the data model
            mediaData.Uploaded = false;
            await repository.UpdateAsync(mediaData.Id, mediaData);
        }

        /// <summary>
        /// Disposes of the media folder 
        /// </summary>
        /// <param name="mediaData"></param>
        public virtual void DisposeContentFolder(TMedia mediaData)
        {
            var path = Path.Combine(ContentFolderPath, mediaData.FolderPath);
            DisposeContentFolder(path);
        }

        /// <summary>
        /// Deletes the media saved to the file system
        /// </summary>
        /// <param name="media"></param>
        public virtual void DisposeContentFile(TMedia media)
        {
            var path = Path.Combine(ContentFolderPath, media.FullPath);
            DisposeContentFile(path);
        }

        /// <summary>
        /// Ensures that the folder for media exists
        /// </summary>
        /// <param name="media"></param>
        protected void EnsureTargetFolderExists(TMedia media)
        {
            EnsureTargetFolderExists(Path.Combine(ContentFolderPath, media.FolderPath))
        }
    }
}
