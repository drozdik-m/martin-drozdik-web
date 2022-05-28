using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Views.Home;

namespace MartinDrozdik.Web.Views.Projects
{
    public class ProjectListItemModel
    {
        public ProjectListItemModel(Project project, ProjectListItemScheme scheme)
        {
            Project = project;
            Scheme = scheme;
        }

        public Project Project { get; }
        public ProjectListItemScheme Scheme { get; }
    }

    public enum ProjectListItemScheme
    {
        Light,
        Dark,
    }
}
