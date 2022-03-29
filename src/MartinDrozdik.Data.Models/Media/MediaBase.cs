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
    public class MediaBase : EntityBase, IIdentifiable<int>
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        [MaxLength(256)]
        public string FolderPath { get; set; } = string.Empty;

        [MaxLength(128)]
        public string FileName { get; set; } = string.Empty;

        public string FullPath => Path.Combine(FolderPath + FileName);

        public string Uri => "/" + FullPath;

        public bool Uploaded { get; set; } = false;
    }
}
