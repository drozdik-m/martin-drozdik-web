using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Abstraction;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Configuration;

namespace MartinDrozdik.Web.Views.Home
{
    public class ProjectsPageModel : ViewModelBase
    {
        public IEnumerable<ProjectTag> ProjectTags { get; }
        public int CompletedProjectsCount { get; }

        public ProjectsPageModel(
            ServerConfiguration serverConfiguration, 
            ICultureProvider cultureProvider, 
            ILanguageDictionary languageDictionary,
            IEnumerable<ProjectTag> projectTags,
            int completedProjectsCount)
            : base(serverConfiguration, cultureProvider, languageDictionary)
        {
            ProjectTags = projectTags;
            CompletedProjectsCount = completedProjectsCount;
        }

        

        public override string Description => "Za svou kariéru mám na kontě celou řadu zajímavých projektů, z nichž část zajisté stojí za zmínku. Projekty jsou různého druhu, od týmových snah přes osobní tvorbu až po odborné texty.";

        public override string PageTitle => "Projekty";

        public override IEnumerable<string> KeywordsList => new string[] { 
            "Martin Drozdík",
            "projekty",
            "teamové práce",
            "papery",
            "weby",
        };

        public override string OgImage => $"{ServerConfiguration.Domain}/Web/_Pages/ProjectsPage/ProjectsPageOG.png";

        public override PageId PageId => PageId.Projects;
    }
}
