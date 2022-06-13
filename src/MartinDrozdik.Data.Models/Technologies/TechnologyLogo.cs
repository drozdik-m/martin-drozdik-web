using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Technologies
{
    public class TechnologyLogo : Image
    {
        public override string FolderPath => $"AppData/TechnologyLogos/{Id}";
    }
}
