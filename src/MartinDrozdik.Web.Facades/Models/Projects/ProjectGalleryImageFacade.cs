﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Repositories.Models.Projects;
using Microsoft.Extensions.Hosting;
using MartinDrozdik.Services.ImageSaving.Configuration;
using MartinDrozdik.Services.ImageSaving;
using MartinDrozdik.Web.Facades.Traits;
using MartinDrozdik.Data.Repositories.Abstraction;

namespace MartinDrozdik.Web.Facades.Models.Projects
{
    public class ProjectGalleryImageFacade : ProjectImageFacade<ProjectGalleryImage>,
        IOrderableFacadeTrait<ProjectGalleryImage, int>
    {
        readonly IOrderableFacadeTrait<ProjectGalleryImage, int> orderableTrait;

        readonly IHostEnvironment hostEnvironment;
        readonly ProjectGalleryImageRepository repository;
        readonly IImageSaver imageSaver;
        readonly IImageConfiguration thumbnailImageConfig = new ImageConfiguration()
        {
            MaxWidth = 500,
            Quality = 75
        };

        public ProjectGalleryImageFacade(IHostEnvironment hostEnvironment,
            ProjectGalleryImageRepository repository,
            IImageSaver imageSaver)
            : base(hostEnvironment, repository, imageSaver, new ImageConfiguration()
            {
                MaxWidth = 1200,
                Quality = 75
            })
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.imageSaver = imageSaver;

            orderableTrait = this;
        }

        public override async Task AddMediaAsync(ProjectGalleryImage mediaData, Stream data, string dataName)
        {
            var dataCopy = new MemoryStream();
            await data.CopyToAsync(dataCopy);

            await base.AddMediaAsync(mediaData, data, dataName);

            var path = Path.Combine(ContentFolderPath, mediaData.ThumbnailFullPath);
            await imageSaver.SaveAsync(path, dataCopy, thumbnailImageConfig);
        }

        #region Orderable trait
        IOrderableRepository<ProjectGalleryImage, int> IOrderableFacadeTrait<ProjectGalleryImage, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);
        #endregion
    }
}
