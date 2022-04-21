using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Services.ImageProcessing.Abstraction;
using Bonsai.Services.ImageProcessing.Abstraction.Configuration;
using Bonsai.Services.ImageProcessing.Configuration;
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
using Bonsai.Services.ImageProcessing;


namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class ProjectOgImageFacade : ProjectImageFacade<ProjectOgImage>
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ProjectOgImageRepository repository;
        readonly IImageSaver imageSaver;

        public ProjectOgImageFacade(IHostEnvironment hostEnvironment,
            ProjectOgImageRepository repository,
            IImageSaver imageSaver)
            : base(hostEnvironment, repository, imageSaver, new ImageConfiguration()
            {
                Width = 1200,
                Quality = 70
            })
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
        }

        public override async Task AddMediaAsync(ProjectOgImage mediaData, Stream data, string dataName)
        {
            const int InnerLogoMaxHeight = 280;

            //Setup the uploaded image
            using var image = Image.Load(data);
            var newImageSize = ImageSaver.GetImageSize(new System.Drawing.Size(image.Width, image.Height), new ImageConfiguration()
            {
                MaxHeight = InnerLogoMaxHeight,
            });
            image.Mutate(o => o.Resize(newImageSize.Width, newImageSize.Height));
            var imageWidth = image.Width;
            var imageHeight = image.Height;

            //Setup the OG image
            var ogTemplatePath = System.IO.Path.Combine(ContentFolderPath, "Templates/Project/ProjectOgTemplate_cs.png");
            using var ogTemplate = Image.Load(ogTemplatePath);
            var ogWidth = ogTemplate.Width;
            var ogHeight = ogTemplate.Height;

            //Create the result image
            using var resultImage = new Image<Rgba32>(1200, 1200);
            resultImage.Mutate(o =>
                o.DrawImage(ogTemplate, new Point(0, 0), 1f)
                 .DrawImage(image, new Point((ogWidth - imageWidth) / 2, 494), 1f)
            );

            using var resultData = new MemoryStream();
            await resultImage.SaveAsPngAsync(resultData);

            //Continue to save the image
            await base.AddMediaAsync(mediaData, resultData, "og-" + dataName);
        }
    }
}
