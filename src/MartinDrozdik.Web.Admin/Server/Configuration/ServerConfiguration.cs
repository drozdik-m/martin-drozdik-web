using System;
using System.Collections.Generic;
using System.Text;
using MartinDrozdik.Data.DbContexts.Configuration;

namespace MartinDrozdik.Web.Admin.Server.Configuration
{
    public class ServerConfiguration
    {
        /// <summary>
        /// Configuration regarding the website
        /// </summary>
        public WebConfiguration Web { get; set; }

        /// <summary>
        /// The domain where the website is currently running
        /// </summary>
        public string Domain { get; set; } = string.Empty;

        /// <summary>
        /// The domain of the MD web
        /// </summary>
        public string WebDomain { get; set; } = string.Empty;

        /// <summary>
        /// Connection strings setup
        /// </summary>
        public ConnectionStringsConfiguration ConnectionStrings { get; set; }

        /// <summary>
        /// User configurations to be seeded when the application starts up
        /// </summary>
        public SeedUserConfiguration[] SeedUsers { get; set; }
    }
}
