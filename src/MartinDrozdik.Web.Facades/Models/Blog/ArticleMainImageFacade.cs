using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Projects;
using Microsoft.Extensions.Hosting;
using MartinDrozdik.Services.ImageSaving.Configuration;
using MartinDrozdik.Services.ImageSaving;
using MartinDrozdik.Web.Facades.Traits;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Web.Facades.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Blog;

namespace MartinDrozdik.Web.Facades.Models.Blog
{
    public class ArticleMainImageFacade : ArticleImageFacade<ArticleMainImage>
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ArticleMainImageRepository repository;
        readonly IImageSaver imageSaver;
        readonly IImageConfiguration thumbnailImageConfig = new ImageConfiguration()
        {
            MaxWidth = 500,
            Quality = 75
        };

        public ArticleMainImageFacade(IHostEnvironment hostEnvironment,
            ArticleMainImageRepository repository,
            IImageSaver imageSaver)
            : base(hostEnvironment, repository, imageSaver, new ImageConfiguration()
            {
                MaxWidth = 1200,
                Quality = 75
            })
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
        }

        public override async Task AddMediaAsync(ArticleMainImage mediaData, Stream data, string dataName)
        {
            await base.AddMediaAsync(mediaData, data, dataName);

            data.Seek(0, SeekOrigin.Begin);

            var path = Path.Combine(ContentFolderPath, mediaData.ThumbnailFullPath);
            await imageSaver.SaveAsync(path, data, thumbnailImageConfig);
        }


        public override void DisposeContentFile(ArticleMainImage media)
        {
            base.DisposeContentFile(media);

            var tmbPath = Path.Combine(ContentFolderPath, media.ThumbnailFullPath);
            DisposeContentFile(tmbPath);
        }
    }
}
