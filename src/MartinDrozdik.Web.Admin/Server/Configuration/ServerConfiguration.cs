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
        /// Connection strings setup
        /// </summary>
        public ConnectionStringsConfiguration ConnectionStrings { get; set; }

        /// <summary>
        /// User configurations to be seeded when the application starts up
        /// </summary>
        public SeedUserConfiguration[] SeedUsers { get; set; }
    }
}
