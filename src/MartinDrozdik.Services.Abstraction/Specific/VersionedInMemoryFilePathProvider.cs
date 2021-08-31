using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MartinDrozdik.Services.FilePathProvider;

namespace MartinDrozdik.Services.FilePathProvider.Specific
{
    public class VersionedInMemoryFilePathProvider : PackedFilePathProvider
    {
        static readonly DateTimeOffset initDatetime = DateTimeOffset.Now;

        /// <inheritdoc/>
        protected string Version
        {
            get
            {
                if (version == null)
                    version = initDatetime.ToUnixTimeMilliseconds().ToString();
                return version;
            }
        }
        private string version = null;

        /// <inheritdoc/>
        public override string PathTo(string path)
        {
            /*var uriBuilder = new UriBuilder(path);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["version"] = Version;
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri.ToString();*/
            return path + "?version=" + Version;
        }
    }
}
