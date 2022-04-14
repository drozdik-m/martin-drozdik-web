using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.AppLogic.Facades.Traits;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Services.ImageProcessing.Abstraction;
using Bonsai.Services.ImageProcessing.Abstraction.Configuration;
using Bonsai.Utils.String;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.Tags;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Media;
using MartinDrozdik.Data.Repositories.Models.Media.Images;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Web.Facades.Models.Media.Images
{
    public class ImageFacade<TMedia> : MediaBaseFacade<TMedia>
        where TMedia : Image
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ImageRepository<TMedia> repository;
        readonly IImageSaver imageSaver;
        readonly IImageConfiguration imageConfiguration;

        public ImageFacade(IHostEnvironment hostEnvironment,
            ImageRepository<TMedia> repository,
            IImageSaver imageSaver,
            IImageConfiguration imageConfiguration)
            : base(hostEnvironment, repository)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
            this.imageConfiguration = imageConfiguration;
        }

        /// <inheritdoc/>
        public override async Task AddMediaAsync(TMedia mediaData, Stream data, string dataName)
        {
            await base.AddMediaAsync(mediaData, data, dataName);

            var path = Path.Combine(hostEnvironment.ContentRootPath, mediaData.FullPath);
            imageSaver.Save(path, System.Drawing.Image.FromStream(data), imageConfiguration);
        }
    }
}
