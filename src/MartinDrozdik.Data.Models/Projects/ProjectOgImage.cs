using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Projects
{
    public class ProjectOgImage : ProjectImage
    {
        public override string FolderPath => $"AppData/ProjectOgImages/{Id}";
    }
}
