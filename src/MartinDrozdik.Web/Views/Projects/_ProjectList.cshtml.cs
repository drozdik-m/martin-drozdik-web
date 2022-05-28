using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Views.Home;

namespace MartinDrozdik.Web.Views.Projects
{
    public class ProjectListModel
    {
        public ProjectListModel(IEnumerable<ProjectTag> tags, ProjectListScheme scheme)
        {
            Tags = tags;
            Scheme = scheme;
        }

        public IEnumerable<ProjectTag> Tags { get; }
        public ProjectListScheme Scheme { get; }

        public bool ShowMoreProjectsButton { get; set; }
    }

    public enum ProjectListScheme
    {
        Light,
        Dark,
    }
}
