using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Projects
{
    public class ProjectGalleryImage : ProjectImage, IOrderable
    {
        /// <inheritdoc />
        public int OrderIndex { get; set; }

        /// <inheritdoc />
        public override string FolderPath => $"AppData/ProjectGalleryImages/{Id}";

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
        public string ThumbnailUri => Path.Combine("/", ThumbnailFullPath);

        /// <summary>
        /// The project this image is assigned to
        /// </summary>
        public Project? Project { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
    }
}
