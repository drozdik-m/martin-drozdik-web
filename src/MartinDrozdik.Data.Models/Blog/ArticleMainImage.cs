using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Blog
{
    public class ArticleMainImage : ArticleImage
    {
        /// <inheritdoc />
        public int OrderIndex { get; set; }

        /// <inheritdoc />
        public override string FolderPath => $"AppData/ArticleMainImages/{Id}";

        /// <summary>
        /// The filename of the media thumbnail
        /// </summary>
        [MaxLength(128)]
        public string ThumbnailFileName => $"thumb_{FileName}";

        /// <summary>
        /// The full path of where the thumbnail media is stored (Folder + ThumbnailName)
        /// </summary>
        public string ThumbnailFullPath => Path.Combine(FolderPath, ThumbnailFileName);

        /// <summary>
        /// The URI of this medias' thumbnail
        /// </summary>
        public string ThumbnailUri => Path.Combine("/", ThumbnailFullPath).Replace("\\", "/");
    }
}
