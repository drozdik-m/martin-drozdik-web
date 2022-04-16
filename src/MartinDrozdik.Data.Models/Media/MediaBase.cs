using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Media
{
    public abstract class MediaBase : EntityBase, IIdentifiable<int>
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// The folder where the media is stored
        /// </summary>
        [MaxLength(256)]
        public abstract string FolderPath { get; }

        /// <summary>
        /// The filename of the media 
        /// </summary>
        [MaxLength(128)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// The full path of where the media is stored (Folder + Name)
        /// </summary>
        public string FullPath => Path.Combine(FolderPath, FileName);

        /// <summary>
        /// The URI of this media
        /// </summary>
        public string Uri => Path.Combine("/", FullPath);

        /// <summary>
        /// Tells if any file has been uploaded or not
        /// </summary>
        public bool Uploaded { get; set; } = false;
    }
}
