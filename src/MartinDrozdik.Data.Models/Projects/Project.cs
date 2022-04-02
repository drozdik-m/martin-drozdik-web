using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Projects
{
    public class Project: EntityBase, IIdentifiable<int>, IOrderable, IHideable
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int OrderIndex { get; set; } = 1;

        public bool IsHidden { get; set; }

        public ICollection<ProjectTag> Tags { get; set; } = new List<ProjectTag>();

        
    }
}
