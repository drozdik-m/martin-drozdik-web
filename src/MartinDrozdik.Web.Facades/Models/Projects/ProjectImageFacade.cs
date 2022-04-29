using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Utils.String;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Tags;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Media;
using MartinDrozdik.Data.Repositories.Models.Media.Images;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Data.Repositories.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Services.ImageSaving;
using MartinDrozdik.Services.ImageSaving.Configuration;
using MartinDrozdik.Web.Facades.Models.Media.Images;
using MartinDrozdik.Web.Facades.Traits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class ProjectImageFacade<TMedia> : ImageFacade<TMedia>
        where TMedia : ProjectImage
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ProjectImageRepository<TMedia> repository;
        readonly IImageSaver imageSaver;
        readonly IImageConfiguration imageConfiguration;

        public ProjectImageFacade(IHostEnvironment hostEnvironment,
            ProjectImageRepository<TMedia> repository,
            IImageSaver imageSaver,
            IImageConfiguration imageConfiguration)
            : base(hostEnvironment, repository, imageSaver, imageConfiguration)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
            this.imageConfiguration = imageConfiguration;
        }
    }
}
