using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;

namespace MartinDrozdik.Data.Models.Media.Images
{
    public class Image : ImageBase, IOrderable, IHideable
    {
        public string Name { get; set; } = string.Empty;

        public int OrderIndex { get; set; } = 0;

        public string AlternativeText { get; set; } = string.Empty;

        public ImageThumbnail? Thumbnail { get; set; } = null;

        public bool HasThumbnail => Thumbnail != null;

        public bool IsHidden { get; set; } = false;
    }
}
