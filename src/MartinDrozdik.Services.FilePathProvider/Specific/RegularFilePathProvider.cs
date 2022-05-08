using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Services.FilePathProvider;

namespace MartinDrozdik.Services.FilePathProvider.Specific
{
    public class RegularFilePathProvider : PackedFilePathProvider
    {
        /// <inheritdoc/>
        public override string PathTo(string path) => path;
    }
}
