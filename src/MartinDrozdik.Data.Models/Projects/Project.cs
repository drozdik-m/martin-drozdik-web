using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Models.People;
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

        public DateTime FinishedTime { get; set; } = DateTime.Now;

        public ProjectLogo Logo { get; set; } = new();

        public ProjectOgImage OgImage { get; set; } = new();

        public ICollection<ProjectTag> Tags { get; set; } = new List<ProjectTag>();
    }
}
