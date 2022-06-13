using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Bonsai.Models.Abstraction;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;

namespace MartinDrozdik.Data.Models.Media.Files
{
    public abstract class MediaFile : MediaBase, IOrderable, IHideable, INameable
    {
        public Image? Image { get; set; } = null;

        public string Name { get; set; }

        public bool HasImage => Image != null;

        public bool IsHidden { get; set; }

        public int OrderIndex { get; set; }

    }
}
