using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Bonsai.Models.Abstraction;
using MartinDrozdik.Abstraction.Entities;

namespace MartinDrozdik.Data.Models.Media.Images
{
    public abstract class Image : MediaBase, INameable
    {
        /// <summary>
        /// The name of the image (title)
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// What text should be displayed when the image could not be loaded
        /// </summary>
        public string AlternativeText { get; set; } = string.Empty;
    }
}
