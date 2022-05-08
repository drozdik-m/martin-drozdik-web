using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bonsai.Utils.JSON;
using MartinDrozdik.Data.Models.Files;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Web.Admin.Client.Services.Models.Media;
using MartinDrozdik.Web.Admin.Client.Services.Traits;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Models.Technologies
{
    public class TechnologyLogoService : MediaBaseService<TechnologyLogo>
    {
        public TechnologyLogoService(HttpClient http)
            : base(http)
        {

        }

        protected override string ApiUri { get; set; } = "/api/TechnologyLogo";
    }
}
