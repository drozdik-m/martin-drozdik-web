using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Projects
{
    public class Project: EntityBase, IIdentifiable<int>, IOrderable, IHideable
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UrlName { get; set; } = string.Empty;

        public int OrderIndex { get; set; } = 0;

        public bool IsHidden { get; set; } = true;

        public string Abstract { get; set; } = string.Empty;

        public bool IsFinished { get; set; } = false;    

        public DateTime? FinishedTime { get; set; } = DateTime.Now;

        public bool HasLiveLink { get; set; } = false;

        public string LiveLink { get ; set; } = string.Empty;

        public bool HasGitHubLink { get; set; } = false;

        public string GitHubLink { get; set; } = string.Empty;

        public ProjectLogo Logo { get; set; } = new();

        [ForeignKey("Logo")]
        public int LogoId { get; set; } 

        public ProjectOgImage OgImage { get; set; } = new();

        [ForeignKey("OgImage")]
        public int OgImageId { get; set; }

        public ProjectPreviewImage PreviewImage { get; set; } = new();

        [ForeignKey("PreviewImage")]
        public int PreviewImageId { get; set; }

        public ProjectMarkdownArticle Article { get; set; } = new();

        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        public ICollection<ProjectHasTag> Tags { get; set; } = new List<ProjectHasTag>();

        public ICollection<ProjectDeveloper> Developers { get; set; } = new List<ProjectDeveloper>();

        public ICollection<ProjectTechnology> Technologies { get; set; } = new List<ProjectTechnology>();

        public ICollection<ProjectGalleryImage> GalleryImages { get; set; } = new List<ProjectGalleryImage>();
    }
}
