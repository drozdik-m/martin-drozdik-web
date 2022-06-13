using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.People
{
    public class PersonProfileImage : PersonImage
    {
        public override string FolderPath => $"AppData/PeopleProfileImages/{Id}";
    }
}
