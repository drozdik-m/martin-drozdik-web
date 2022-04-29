using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Projects;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Drawing.Processing.Processors.Drawing;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors;
using MartinDrozdik.Services.ImageSaving;
using MartinDrozdik.Services.ImageSaving.Configuration;

namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class ProjectPreviewImageFacade : ProjectImageFacade<ProjectPreviewImage>
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ProjectPreviewImageRepository repository;
        readonly IImageSaver imageSaver;

        public ProjectPreviewImageFacade(IHostEnvironment hostEnvironment,
            ProjectPreviewImageRepository repository,
            IImageSaver imageSaver)
            : base(hostEnvironment, repository, imageSaver, new ImageConfiguration()
            {
                MaxWidth = 540,
                Quality = 70
            })
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
        }
    }
}
