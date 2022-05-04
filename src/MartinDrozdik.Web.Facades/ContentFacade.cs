using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Models.Media.Exceptions;
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
        public virtual void DisposeContentFolder(string path, bool disposeContents = false)
        {
            if (Directory.Exists(path) && File.GetAttributes(path) == FileAttributes.Directory)
                Directory.Delete(path, disposeContents);
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

        /// <summary>
        /// Ensures the target path is ok to use and free of any existing files
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="Exception"></exception>
        protected void EnsureSafePath(string path, bool overwrite = false)
        {
            if (overwrite)
                DisposeContentFile(path);
            else
            {
                if (File.Exists(path))
                    throw new FileAlreadyExistsException("File with this name already exists");
            }
            

            var dirPath = Path.GetDirectoryName(path);
            if (dirPath is not null)
                EnsureTargetFolderExists(dirPath);
        }

        /// <summary>
        /// Ensures that the folder exists
        /// </summary>
        /// <param name="path"></param>
        protected void EnsureTargetFolderExists(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Saves a stream to a file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="stream"></param>
        /// <param name="overwrite"></param>
        protected void SaveStreamAsFile(string path, Stream stream, bool overwrite = false)
        {
            EnsureSafePath(path, overwrite);
            using FileStream outputFileStream = new FileStream(path, FileMode.Create);
            stream.CopyTo(outputFileStream);
        }
    }
}
