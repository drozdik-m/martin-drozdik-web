using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Projects
{
    public class ProjectLogo : ProjectImage
    {
        public override string FolderPath => $"AppData/ProjectLogos/{Id}";
    }
}
