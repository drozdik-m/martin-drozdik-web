using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Services.FilePathProvider
{
    public abstract class PackedFilePathProvider : IFilePathProvider
    {
        /// <inheritdoc/>
        public string CleanPathTo(string path) => path;

        /// <inheritdoc/>
        public string CleanPathTo(string path, string razorLibName)
        {
            if (!path.StartsWith("/"))
                path = "/" + path;
            return CleanPathTo("/_content/" + razorLibName + path);
        }

        /// <inheritdoc/>
        public abstract string PathTo(string path);

        /// <inheritdoc/>
        public string PathTo(string path, string razorLibName)
        {
            if (!path.StartsWith("/"))
                path = "/" + path;
            return PathTo("/_content/" + razorLibName + path);
        }
    }
}
