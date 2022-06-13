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
using MartinDrozdik.Web.Facades.Models.Markdown;
using Markdig;

namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class ProjectMarkdownArticleFacade : MarkdownArticleFacade<ProjectMarkdownArticle>
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ProjectMarkdownArticleRepository repository;
        readonly IImageSaver imageSaver;
        /*readonly IImageConfiguration thumbnailImageConfig = new ImageConfiguration()
        {
            MaxWidth = 500,
            Quality = 75
        };*/

       
        public ProjectMarkdownArticleFacade(IHostEnvironment hostEnvironment,
            ProjectMarkdownArticleRepository repository,
            IImageSaver imageSaver)
            : base(hostEnvironment, imageSaver, repository)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
        }

        

    }
}
