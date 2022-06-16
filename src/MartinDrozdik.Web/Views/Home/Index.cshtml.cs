using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Abstraction;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Configuration;

namespace MartinDrozdik.Web.Views.Home
{
    public class IndexPageModel : ViewModelBase
    {
        public IEnumerable<WorkExperience> WorkExperiences { get; }
        public IEnumerable<Education> Educations { get; }
        public IEnumerable<LanguageSkill> LanguageSkills { get; }
        public IEnumerable<Project> PreviewProjects { get; }
        public IEnumerable<Article> PreviewArticles { get; }

        public IndexPageModel(
            ServerConfiguration serverConfiguration,
            ICultureProvider cultureProvider, 
            ILanguageDictionary languageDictionary,
            IEnumerable<WorkExperience> workExperiences,
            IEnumerable<Education> educations,
            IEnumerable<LanguageSkill> languageSkills,
            IEnumerable<Project> previewProjects,
            IEnumerable<Article> previewArticles)
            : base(serverConfiguration, cultureProvider, languageDictionary)
        {
            WorkExperiences = workExperiences;
            Educations = educations;
            LanguageSkills = languageSkills;
            PreviewProjects = previewProjects;
            PreviewArticles = previewArticles;
        }

        public override string Description => "Jmenuji se Martin Drozdík a jsem softwarový engineer. Programuji již od malička. Mám širokou škálu zkušeností s programování webových stránek, a to jak front-end, tak back-end. Umím si však poradit s programováním všeho druhu, díky kvalitnímu object-oriented designu a modularizaci.";

        public override string PageTitle => "Martin Drozdík | Software Engineer | Portfolio";

        public override string Title => PageTitle;

        public override IEnumerable<string> KeywordsList => new string[] { 
            "Martin Drozdík",
            "Software engineer",
            "OOP",
            "programátor",
            "portfolio",
            "osobní web",
            "projekty",
            "blog"
        };

        public override string OgImage => $"{ServerConfiguration.Domain}/Web/_Pages/IndexPage/IndexOG.png";

        public override PageId PageId => PageId.Index;

        
    }
}
