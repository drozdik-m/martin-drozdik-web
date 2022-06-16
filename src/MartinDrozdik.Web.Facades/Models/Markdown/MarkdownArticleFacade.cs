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
            MaxWidth = 800,
            Quality = 75
        };

        readonly IImageConfiguration textWidthImageConfig = new ImageConfiguration
        {
            MaxWidth = 1000,
            Quality = 75
        };

        readonly IImageConfiguration wideImageConfig = new ImageConfiguration
        {
            MaxWidth = 1300,
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
            .UseMediaLinks()
            .UseAbbreviations()
            .UseFootnotes()
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

            //Mark external links and file types
            var xml = XElement.Parse($"<root>{article.HTML}</root>");
            var links = xml.Descendants("a");
            foreach(var link in links)
            {
                var classVal = link.Attribute("class")?.Value;
                var href = link.Attribute("href")?.Value;

                var isFile = false;
                if (classVal is not null && classVal.Contains("file"))
                    isFile = true;

                if (href is null)
                    continue;

                if (href.StartsWith("http://") || href.StartsWith("https://"))
                {
                    link.SetAttributeValue("target", "_blank");
                    link.SetAttributeValue("rel", "noopener");
                    link.SetAttributeValue("class", "external");
                }

                if (isFile)
                {
                    link.SetAttributeValue("target", "_blank");

                    var extension = Path.GetExtension(href);
                    if (extension is not null)
                    {
                        extension = extension.ToLower().Replace(".", "");
                        link.SetAttributeValue("data-file-type", extension);
                    }
                }
                
                
            }

            //Add titles to links
            foreach (var link in links)
            {
                var classVal = link.Attribute("class")?.Value;
                var href = link.Attribute("href")?.Value;

                var isFile = false;
                if (classVal is not null && classVal.Contains("file"))
                    isFile = true;

                if (href is not null)
                {
                    if (!isFile)
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
            var usedMedia = ExtractUsedMedia(article, out var usedImages, out var usedFiles);
            var savedImages = GetSavedImages(article);
            var savedFiles = GetSavedFiles(article);

            var imagesToDelete = savedImages.Where(e => !usedImages.Contains(e));
            foreach (var media in imagesToDelete)
            {
                var mediaRelativePath = article.GetImagePath(media);
                DisposeContentFile(Path.Combine(ContentFolderPath, mediaRelativePath));
            }

            var filesToDelete = savedFiles.Where(e => !usedFiles.Contains(e));
            foreach (var media in filesToDelete)
            {
                var mediaRelativePath = article.GetFilePath(media);
                DisposeContentFile(Path.Combine(ContentFolderPath, mediaRelativePath));
            }
        }

        /// <summary>
        /// Returns list of all links and image sources
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public virtual List<string> ExtractUsedMedia(TArticle article, out List<string> images, out List<string> files)
        {
            var result = new List<string>();

            var xml = XElement.Parse($"<root>{article.HTML}</root>");

            //Images
            var imgSrcs = xml
                       .Descendants("img")
                       .Select(x => x.Attribute("src")?.Value)
                       .Where(e => e is not null)
                       .Select(Path.GetFileName)
                       .OfType<string>()
                       .ToList();
            result.AddRange(imgSrcs);
            images = imgSrcs;

            //Files
            var hrefLinks = xml
                       .Descendants("a")
                       .Where(e =>
                       {
                           var classVal = e.Attribute("class")?.Value;
                           if (classVal is not null && classVal.Contains("file"))
                               return true;
                           return false;
                       })
                       .Select(x => x.Attribute("href")?.Value)
                       .Where(e => e is not null)
                       .Select(Path.GetFileName)
                       .Where(e => e is not null)
                       .OfType<string>()
                       
                       .ToList();
            result.AddRange(hrefLinks);
            files = hrefLinks;

            return result;
        }

        /// <summary>
        /// Returns name list of all image sources
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public virtual List<string> GetSavedImages(TArticle article)
            => GetSavedMedia(article, article.ImagesFolderPath);

        /// <summary>
        /// Returns name list of all links sources
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public virtual List<string> GetSavedFiles(TArticle article)
            => GetSavedMedia(article, article.FilesFolderPath);
        /// <summary>
        /// Returns name list of a saved media
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public virtual List<string> GetSavedMedia(TArticle article, string folderPath)
        {
            var result = new List<string>();

            //Files
            var filesPath = Path.Combine(ContentFolderPath, folderPath);
            if (Directory.Exists(filesPath))
            {
                IEnumerable<string> files = Directory.GetFiles(filesPath);
                files = files
                    .Select(Path.GetFileName)
                    .Where(e => e is not null)
                    .OfType<string>();
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

        /// <summary>
        /// Updates paths of resources
        /// </summary>
        /// <param name="article"></param>
        /// <param name="linkFactory"></param>
        /// <returns></returns>
        public TArticle WithUpdatedResourceLinks(TArticle article, Func<string, string> linkFactory)
        {
            //Update image paths
            var xml = XElement.Parse($"<root>{article.HTML}</root>");
            var images = xml.Descendants("img");
            foreach (var link in images)
            {
                var src = link.Attribute("src")?.Value;
                if (src is not null && !src.StartsWith("http://") && !src.StartsWith("https://"))
                    link.SetAttributeValue("src", linkFactory(src));
            }

            //Update file paths
            var links = xml.Descendants("a");
            foreach (var link in links)
            {
                var classVal = link.Attribute("class")?.Value;

                var isFile = false;
                if (classVal is not null && classVal.Contains("file"))
                    isFile = true;

                if (!isFile)
                    continue;

                var src = link.Attribute("href")?.Value;
                if (src is not null && !src.StartsWith("http://") && !src.StartsWith("https://"))
                    link.SetAttributeValue("href", linkFactory(src));
            }

            //Save the XML
            var reader = xml.CreateReader();
            reader.MoveToContent();
            article.HTML = reader.ReadInnerXml();

            return article;
        }

        /// <summary>
        /// Updates paths of resources
        /// </summary>
        /// <param name="article"></param>
        /// <param name="linkFactory"></param>
        /// <returns></returns>
        public IEnumerable<TArticle> WithUpdatedResourceLinks(IEnumerable<TArticle> articles, Func<string, string> linkFactory)
            => articles.Select(e => WithUpdatedResourceLinks(e, linkFactory));
    }
}
