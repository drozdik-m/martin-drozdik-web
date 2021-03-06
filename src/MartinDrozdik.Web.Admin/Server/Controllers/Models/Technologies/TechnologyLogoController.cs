using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Server.Controllers.BaseControllers;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Web.Admin.Server.Controllers.Models.Media.Images;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.Media;
using MartinDrozdik.Web.Facades.Models.Media.Images;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Technologies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.Technologies
{
    public class TechnologyLogoController : ImageController<TechnologyLogo>
    {
        readonly TechnologyLogoFacade facade;

        public TechnologyLogoController(TechnologyLogoFacade facade)
            : base(facade)
        {
            this.facade = facade;
        }
    }
}
