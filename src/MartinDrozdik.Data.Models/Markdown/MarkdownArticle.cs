using System;
using System.Collections.Generic;
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
    }
}
