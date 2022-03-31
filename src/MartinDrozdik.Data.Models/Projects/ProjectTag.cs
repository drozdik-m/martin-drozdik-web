using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Tags;

namespace MartinDrozdik.Data.Models.Projects
{
    public class ProjectTag : Tag
    {
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
