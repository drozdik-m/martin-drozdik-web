using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.People
{
    public class PersonImage : Image
    {
        public override string FolderPath => $"/AppData/People/{Id}";
    }
}
