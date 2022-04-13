using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.AppLogic.Facades.Traits;
using Bonsai.Models.Abstraction.Localization;
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

        public void AddMedia()
        {
            throw new NotImplementedException();
        }

        public void DeleteMedia()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the media saved to the file system
        /// </summary>
        /// <param name="media"></param>
        public void DeletePhysicalMediaFile(TMedia media)
        {
            var path = Path.Combine(hostEnvironment.ContentRootPath, media.FullPath);
            if (File.Exists(path) && File.GetAttributes(path) != FileAttributes.Directory)
                File.Delete(path);
        }

        /// <summary>
        /// Ensures that the folder for media exists
        /// </summary>
        /// <param name="media"></param>
        public void EnsureTargetFolderExists(TMedia media)
        {
            Directory.CreateDirectory(Path.Combine(hostEnvironment.ContentRootPath, media.FolderPath));
        }
    }
}
