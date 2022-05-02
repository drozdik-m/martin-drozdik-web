using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Abstraction;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Web.Facades
{
    public class ContentFacade<TEntity, TKey> : CRUDFacade<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>
    {
        readonly IHostEnvironment hostEnvironment;
        readonly ICRUDRepository<TEntity, TKey> repository;

        public ContentFacade(IHostEnvironment hostEnvironment,
            ICRUDRepository<TEntity, TKey> repository) : base(repository)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
        }

        /// <summary>
        /// Path to the folder with content
        /// </summary>
        public string ContentFolderPath => Path.Combine(hostEnvironment.ContentRootPath, "wwwroot/");

        /// <summary>
        /// Disposes a target folder
        /// </summary>
        /// <param name="path"></param>
        public virtual void DisposeContentFolder(string path)
        {
            if (Directory.Exists(path) && File.GetAttributes(path) == FileAttributes.Directory)
                Directory.Delete(path);
        }

        // <summary>
        /// Deletes the media saved to the file system
        /// </summary>
        /// <param name="path"></param>
        public virtual void DisposeContentFile(string path)
        {
            if (File.Exists(path) && File.GetAttributes(path) != FileAttributes.Directory)
                File.Delete(path);
        }
    }
}
