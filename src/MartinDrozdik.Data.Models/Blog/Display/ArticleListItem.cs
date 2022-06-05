using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Blog.Display
{
    public class ArticleListItem : IIdentifiable<int>
    {
        public int Id { get; set; }

        public List<int> Tags { get; set; } = new List<int>();

        public string HTML { get; set; } = string.Empty;
    }
}
