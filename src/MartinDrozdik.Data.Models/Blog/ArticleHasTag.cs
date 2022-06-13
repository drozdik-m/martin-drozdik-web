using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Models;
using Newtonsoft.Json;

namespace MartinDrozdik.Data.Models.Blog
{
    public class ArticleHasTag : EntityBase, IIdentifiable<int>
    {
        public int Id { get; set; }

        public int ArticlesId { get; set; }
        [ForeignKey("ArticlesId")]
        public Article? Project { get; set; }


        public int TagsId { get; set; }
        [ForeignKey("TagsId")]
        public ArticleTag? Tag { get; set; }
    }
}
