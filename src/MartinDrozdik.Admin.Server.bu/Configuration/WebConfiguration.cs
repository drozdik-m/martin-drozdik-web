using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Web.Configuration
{
    public class WebConfiguration
    {
        /// <summary>
        /// Name of this website
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The domain where the website is currently running
        /// </summary>
        public string Domain { get; set; }
    }
}
