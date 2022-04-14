﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.AppLogic.Facades.Traits;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Services.ImageProcessing.Abstraction;
using Bonsai.Services.ImageProcessing.Abstraction.Configuration;
using Bonsai.Utils.String;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Tags;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Media;
using MartinDrozdik.Data.Repositories.Models.Media.Images;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Web.Facades.Models.Media.Images;
using MartinDrozdik.Web.Facades.Traits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class PersonImageFacade<TMedia> : ImageFacade<TMedia>
        where TMedia : PersonImage
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ImageRepository<TMedia> repository;
        readonly IImageSaver imageSaver;
        readonly IImageConfiguration imageConfiguration;

        public PersonImageFacade(IHostEnvironment hostEnvironment,
            ImageRepository<TMedia> repository,
            IImageSaver imageSaver,
            IImageConfiguration imageConfiguration)
            : base(hostEnvironment, repository, imageSaver, imageConfiguration)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;
            this.imageConfiguration = imageConfiguration;
        }
    }
}
