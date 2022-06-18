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
    public class ProjectPageModel : ViewModelBase
    {
        public Data.Models.Projects.Project Project { get; }

        public ProjectPageModel(
            ServerConfiguration serverConfiguration,
            ICultureProvider cultureProvider, 
            ILanguageDictionary languageDictionary,
            Data.Models.Projects.Project project)
            : base(serverConfiguration, cultureProvider, languageDictionary)
        {
            Project = project;
        }


        public override string PageType => "article";

        public override DateTime? RevisionDate => Project.LastEditAt;

        public override string Description => Project.Abstract;

        public override string PageTitle => Project.Name;

        public override IEnumerable<string> KeywordsList => new string[] { 
            Project.Keywords
        }.Concat(Project.Tags.Select(e => e.Tag.Name));

        public override string OgImage => ServerConfiguration.ContentDomain + Project.OgImage.Uri;

        public override PageId PageId => PageId.Project;

        public static string FinishedDateToString(DateTime finishedDate)
        {
            var monthWord = finishedDate.Month switch
            {
                1  => "lednu",
                2  => "únoru",
                3  => "březnu",
                4  => "dubnu",
                5  => "květnu",
                6  => "červnu",
                7  => "červenci",
                8  => "srpnu",
                9  => "září",
                10 => "říjnu",
                11 => "listopadu",
                12 => "prosinci",
                _ => throw new Exception("Unknown month"),
            };

            return $"{monthWord} {finishedDate.Year}";
        }
    }
}
