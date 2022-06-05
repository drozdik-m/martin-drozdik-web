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
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Repositories.Models.Blog;

namespace MartinDrozdik.Web.Facades.Models.Blog
{
    public class BlogMarkdownArticleFacade : MarkdownArticleFacade<BlogMarkdownArticle>
    {
        readonly IHostEnvironment hostEnvironment;
        readonly BlogMarkdownArticleRepository repository;
        readonly IImageSaver imageSaver;


        public BlogMarkdownArticleFacade(IHostEnvironment hostEnvironment,
            BlogMarkdownArticleRepository repository,
            IImageSaver imageSaver)
            : base(hostEnvironment, imageSaver, repository)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
        }



    }
}
