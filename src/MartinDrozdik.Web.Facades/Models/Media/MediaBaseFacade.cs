using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.AppLogic.Facades.Traits;
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
    public class MediaBaseFacade<TMedia> : CRUDFacade<TMedia, int>
        where TMedia : MediaBase
    {
        readonly IHostEnvironment hostEnvironment;
        readonly MediaBaseRepository<TMedia> repository;

        public MediaBaseFacade(IHostEnvironment hostEnvironment,
            MediaBaseRepository<TMedia> repository)
            : base(repository)
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
            DeletePhysicalMediaFile(mediaData);

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
        public async Task DeleteMediaAsync(TMedia mediaData)
        {
            //Delete the physical media
            DeletePhysicalMediaFile(mediaData);

            //Update the data model
            mediaData.Uploaded = false;
            await repository.UpdateAsync(mediaData.Id, mediaData);
        }

        /// <summary>
        /// Deletes the media saved to the file system
        /// </summary>
        /// <param name="media"></param>
        protected void DeletePhysicalMediaFile(TMedia media)
        {
            var path = Path.Combine(hostEnvironment.ContentRootPath, media.FullPath);
            if (File.Exists(path) && File.GetAttributes(path) != FileAttributes.Directory)
                File.Delete(path);
        }

        /// <summary>
        /// Ensures that the folder for media exists
        /// </summary>
        /// <param name="media"></param>
        protected void EnsureTargetFolderExists(TMedia media)
        {
            Directory.CreateDirectory(Path.Combine(hostEnvironment.ContentRootPath, media.FolderPath));
        }
    }
}
