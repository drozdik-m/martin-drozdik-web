using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Markdown
{
    public abstract class MarkdownArticle : EntityBase, IIdentifiable<int>
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// The markdown content
        /// </summary>
        public string Markdown { get; set; } = string.Empty;

        /// <summary>
        /// Markdown translated into HTML
        /// </summary>
        public string HTML { get; set; } = string.Empty;

        /// <summary>
        /// The path to the folder with images
        /// </summary>
        public abstract string ImagesFolderPath { get; }

        /// <summary>
        /// Returns Uri for an image
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public string GetImageUri(string imageName)
            => Path.Combine(ImagesFolderPath, imageName).Replace("\\", "/");

        /// <summary>
        /// The path to the folder with files
        /// </summary>
        public abstract string FilesFolderPath { get; }

        /// <summary>
        /// Returns Uri for a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFileUri(string fileName)
            => Path.Combine(FilesFolderPath, fileName).Replace("\\", "/");
    }
}
