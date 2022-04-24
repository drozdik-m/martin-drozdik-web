using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Models;
using Newtonsoft.Json;

namespace MartinDrozdik.Data.Models.Projects
{
    public class ProjectHasTag : EntityBase, IIdentifiable<int>
    {
        public int Id { get; set; }

        public int ProjectsId { get; set; }
        [ForeignKey("ProjectsId")]
        public Project? Project { get; set; }


        public int TagsId { get; set; }
        [ForeignKey("TagsId")]
        public ProjectTag? Tag { get; set; }
    }
}
