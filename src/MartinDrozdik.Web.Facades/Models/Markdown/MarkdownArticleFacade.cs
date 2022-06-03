using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Bonsai.Utils.String;
using Markdig;
using MartinDrozdik.Data.Models.Markdown;
using MartinDrozdik.Data.Models.Markdown.Media;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Tags;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Media;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Services.ImageSaving;
using MartinDrozdik.Services.ImageSaving.Configuration;
using MartinDrozdik.Web.Facades.Traits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Web.Facades.Models.Markdown
{
    public class MarkdownArticleFacade<TArticle> : ContentFacade<TArticle, int>
        where TArticle : MarkdownArticle
    {
        readonly IHostEnvironment hostEnvironment;
        readonly IImageSaver imageSaver;
        readonly MarkdownArticleRepository<TArticle> repository;

        readonly IImageConfiguration regularImageConfig = new ImageConfiguration
        {
            MaxWidth = 1400,
            Quality = 75
        };

        readonly IImageConfiguration textWidthImageConfig = new ImageConfiguration
        {
            MaxWidth = 1200,
            Quality = 75
        };

        readonly IImageConfiguration wideImageConfig = new ImageConfiguration
        {
            MaxWidth = 800,
            Quality = 75
        };

        static readonly MarkdownPipeline markdownPipeline =
            new MarkdownPipelineBuilder()
            .UseEmphasisExtras()
            .UseCustomContainers()
            .UseGenericAttributes()
            .UseFootnotes()
            .UseAutoIdentifiers()
            .UseAutoLinks()
            .UseMathematics()
            .UseEmojiAndSmiley()
            .UseDiagrams()
            .UseCitations()
            .UseFigures()
            .Build();


        public MarkdownArticleFacade(IHostEnvironment hostEnvironment,
            IImageSaver imageSaver,
            MarkdownArticleRepository<TArticle> repository)
            : base(hostEnvironment, repository)
        {
            this.hostEnvironment = hostEnvironment;
            this.imageSaver = imageSaver;
            this.repository = repository;
        }

        /// <inheritdoc />
        public override async Task AddAsync(TArticle item)
        {
            item = UpdateHTML(item);
            await base.AddAsync(item);
        }

        /// <inheritdoc />
        public override async Task UpdateAsync(int id, TArticle item)
        {
            UpdateArticlesContent(item);
            await base.UpdateAsync(id, item);
        }

        /// <inheritdoc />
        public override async Task DeleteAsync(int id)
        {
            var item = await repository.ReadAsync(id);
            DisposeContents(item);
            await base.DeleteAsync(id);
        }

        /// <summary>
        /// Updates all contents of the article (HTML, file contents, ...)
        /// </summary>
        /// <param name="article"></param>
        public virtual void UpdateArticlesContent(TArticle article)
        {
            article = UpdateHTML(article);
            RemoveUnusedMedia(article);

        }

        /// <summary>
        /// Disposes all resources of an article
        /// </summary>
        /// <param name="article"></param>
        public virtual void DisposeContents(TArticle article)
        {
            DisposeContentFolder(Path.Combine(ContentFolderPath, article.ImagesFolderPath), disposeContents: true);
            DisposeContentFolder(Path.Combine(ContentFolderPath, article.FilesFolderPath), disposeContents: true);
        }

        /// <summary>
        /// Converts a markdown string into a HTML string
        /// </summary>
        /// <param name="markdown"></param>
        /// <returns></returns>
        protected virtual string MarkdownToHTML(string markdown)
        {
            return Markdig.Markdown.ToHtml(markdown, markdownPipeline);
        }

        /// <summary>
        /// Updates the HTML inside an article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public virtual TArticle UpdateHTML(TArticle article)
        {
            //Get HTML
            article.HTML = MarkdownToHTML(article.Markdown);

            //Mark external links
            var xml = XElement.Parse($"<root>{article.HTML}</root>");
            var links = xml.Descendants("a");
            foreach(var link in links)
            {
                var href = link.Attribute("href")?.Value;
                if (href is not null && (href.StartsWith("http://") || href.StartsWith("https://")))
                {
                    link.SetAttributeValue("target", "_blank");
                    link.SetAttributeValue("rel", "noopener");
                    link.SetAttributeValue("class", "external");
                }
            }

            //Add titles to links
            foreach (var link in links)
            {
                var href = link.Attribute("href")?.Value;
                if (href is not null)
                {
                    link.SetAttributeValue("title", $"Link: {href}");
                }
            }

            //Save the XML
            var reader = xml.CreateReader();
            reader.MoveToContent();
            article.HTML = reader.ReadInnerXml();

            return article;
        }

        /// <summary>
        /// Removes all files and/or images that are not used in the HTML content
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public virtual void RemoveUnusedMedia(TArticle article)
        {
            var usedMedia = ExtractUsedMedia(article);
            var savedMedia = GetSavedMedia(article);

            var mediaToDelete = savedMedia.Where(e => !usedMedia.Contains(e));

            foreach(var media in mediaToDelete)
                DisposeContentFile(Path.Combine(ContentFolderPath, media));
        }

        /// <summary>
        /// Returns list of all links and image sources
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public virtual List<string> ExtractUsedMedia(TArticle article)
        {
            var result = new List<string>();

            var xml = XElement.Parse($"<root>{article.HTML}</root>");

            //Images
            var imgSrcs = xml
                       .Descendants("img")
                       .Select(x => x.Attribute("src")?.Value)
                       .Where(e => e is not null)
                       .OfType<string>()
                       .ToList();
            result.AddRange(imgSrcs);

            //Files
            var hrefLinks = xml
                       .Descendants("a")
                       .Select(x => x.Attribute("href")?.Value)
                       .Where(e => e is not null)
                       .OfType<string>()
                       .ToList();
            result.AddRange(hrefLinks);

            return result;
        }

        /// <summary>
        /// Returns list of all links and image sources
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public virtual List<string> GetSavedMedia(TArticle article)
        {
            var result = new List<string>();

            //Images
            var imagesPath = Path.Combine(ContentFolderPath, article.ImagesFolderPath);
            if (Directory.Exists(imagesPath))
            {
                IEnumerable<string> files = Directory.GetFiles(imagesPath);
                files = files.Select(e => article.GetImageUri(Path.GetFileName(e)));
                result.AddRange(files);
            }

            //Files
            var filesPath = Path.Combine(ContentFolderPath, article.FilesFolderPath);
            if (Directory.Exists(filesPath))
            {
                IEnumerable<string> files = Directory.GetFiles(filesPath);
                files = files.Select(e => article.GetFileUri(Path.GetFileName(e)));
                result.AddRange(files);
            }

            return result;
        }

        /// <summary>
        /// Adds a file to the articles' file folder
        /// </summary>
        /// <param name="article"></param>
        /// <param name="data"></param>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public Task<AddFileResponse> AddFileAsync(TArticle article, Stream data, string dataName)
        {
            dataName = dataName.ToUrlFriendlyFileName();
            var path = Path.Combine(ContentFolderPath, article.FilesFolderPath, dataName);
            SaveStreamAsFile(path, data);

            return Task.FromResult(new AddFileResponse()
            {
                MediaURI = article.GetFileUri(dataName),
            });
        }

        /// <summary>
        /// Adds a file to the articles' file folder
        /// </summary>
        /// <param name="article"></param>
        /// <param name="data"></param>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public virtual async Task<AddFileResponse> AddFileAsync(TArticle article, IEnumerable<byte> data, string dataName)
        {
            using Stream stream = new MemoryStream(data.ToArray());
            return await AddFileAsync(article, stream, dataName);
        }

        /// <summary>
        /// Adds an image to the articles' image folder
        /// </summary>
        /// <param name="article"></param>
        /// <param name="data"></param>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public async Task<AddImageResponse> AddImageAsync(TArticle article, ImageSize size, Stream data, string dataName)
        {
            dataName = dataName.ToUrlFriendlyFileName();
            var path = Path.Combine(ContentFolderPath, article.ImagesFolderPath, dataName);
            EnsureSafePath(path);
            
            await imageSaver.SaveAsync(path, data, GetConfigurationForImageSize(size));
            return new AddImageResponse()
            {
                MediaURI = article.GetImageUri(dataName),
            };
        }

        /// <summary>
        /// Adds an image to the articles' image folder
        /// </summary>
        /// <param name="article"></param>
        /// <param name="data"></param>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public virtual async Task<AddImageResponse> AddImageAsync(TArticle article, ImageSize size, IEnumerable<byte> data, string dataName)
        {
            using Stream stream = new MemoryStream(data.ToArray());
            return await AddImageAsync(article, size, stream, dataName);
        }

        /// <summary>
        /// Returns appropriate image configuration for an image size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IImageConfiguration GetConfigurationForImageSize(ImageSize size)
        {
            return size switch
            {
                ImageSize.Regular => regularImageConfig,
                ImageSize.TextWidth => textWidthImageConfig,
                ImageSize.Wide => wideImageConfig,
                _ => throw new Exception("Unknown value of ImageSize enum"),
            };
        }

    }
}
