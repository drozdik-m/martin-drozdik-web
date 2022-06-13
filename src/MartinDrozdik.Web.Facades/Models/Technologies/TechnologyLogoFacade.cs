using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bonsai.Utils.String;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Tags;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Media;
using MartinDrozdik.Data.Repositories.Models.Media.Images;
using MartinDrozdik.Data.Repositories.Models.People;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Data.Repositories.Models.Technologies;
using MartinDrozdik.Services.ImageSaving;
using MartinDrozdik.Services.ImageSaving.Configuration;
using MartinDrozdik.Web.Facades.Models.Media.Images;
using MartinDrozdik.Web.Facades.Traits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Web.Facades.Models.Technologies
{
    public class TechnologyLogoFacade : ImageFacade<TechnologyLogo>
    {
        readonly IHostEnvironment hostEnvironment;
        readonly TechnologyLogoRepository repository;
        readonly IImageSaver imageSaver;

        public TechnologyLogoFacade(IHostEnvironment hostEnvironment,
            TechnologyLogoRepository repository,
            IImageSaver imageSaver)
            : base(hostEnvironment, repository, imageSaver, new ImageConfiguration()
            {
                MaxWidth = 256,
                Quality = 80
            })
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
        }
    }
}
