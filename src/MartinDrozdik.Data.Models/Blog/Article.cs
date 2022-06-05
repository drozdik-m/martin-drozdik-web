using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Blog
{
    public class Article : EntityBase, IIdentifiable<int>, IOrderable, IHideable
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UrlName { get; set; } = string.Empty;

        public int OrderIndex { get; set; } = 0;

        public bool IsHidden { get; set; } = true;

        public string Abstract { get; set; } = string.Empty;

        public string Keywords { get; set; } = string.Empty;

        public ArticleMainImage MainImage { get; set; } = new();

        [ForeignKey("MainImage")]
        public int MainImageId { get; set; }

        public BlogMarkdownArticle Content { get; set; } = new();

        [ForeignKey("Content")]
        public int ContentId { get; set; }

        public Person Author { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public ICollection<ProjectHasTag> Tags { get; set; } = new List<ProjectHasTag>();


    }
}
