using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bonsai.Utils.String;
using MartinDrozdik.Data.Models.Blog;
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

namespace MartinDrozdik.Web.Facades.Models.Blog
{
    public class ArticleImageFacade<TMedia> : ImageFacade<TMedia>
        where TMedia : ArticleImage
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ArticleImageRepository<TMedia> repository;
        readonly IImageSaver imageSaver;
        readonly IImageConfiguration imageConfiguration;

        public ArticleImageFacade(IHostEnvironment hostEnvironment,
            ArticleImageRepository<TMedia> repository,
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
