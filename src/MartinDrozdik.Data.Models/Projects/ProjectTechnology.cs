using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Models;
using Newtonsoft.Json;

namespace MartinDrozdik.Data.Models.Projects
{
    public class ProjectTechnology : EntityBase, IIdentifiable<int>, IOrderable
    {
        public int Id { get; set; }

        public int OrderIndex { get; set; }

        public int ProjectsId { get; set; }
        [ForeignKey("ProjectsId")]
        public Project? Project { get; set; }


        public int TechnologiesId { get; set; }
        [ForeignKey("TechnologiesId")]
        public Technology? Technology { get; set; }

        public string Usage { get; set; } = string.Empty;
    }
}
