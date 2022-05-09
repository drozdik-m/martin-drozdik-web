using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Abstraction;
using MartinDrozdik.Data.Models.CV;

namespace MartinDrozdik.Web.Views.Home
{
    public class IndexPageModel : ViewModelBase
    {
        public IEnumerable<WorkExperience> WorkExperiences { get; }
        public IEnumerable<Education> Educations { get; }
        public IEnumerable<LanguageSkill> LanguageSkills { get; }

        public IndexPageModel(ICultureProvider cultureProvider, 
            ILanguageDictionary languageDictionary,
            IEnumerable<WorkExperience> workExperiences,
            IEnumerable<Education> educations,
            IEnumerable<LanguageSkill> languageSkills)
            : base(cultureProvider, languageDictionary)
        {
            WorkExperiences = workExperiences;
            Educations = educations;
            LanguageSkills = languageSkills;
        }

        public override string Description => "Jmenuji se Martin Drozdík a jsem softwarový engineer. Programuji již od malička. Mám širokou škálu zkušeností s programování webových stránek, a to jak front-end, tak back-end. Umím si však poradit s programováním všeho druhu, díky kvalitnímu object-oriented designu a modularizaci.";

        public override string PageTitle => "Martin Drozdík | Software Engineer | Portfolio";

        public override string Title => PageTitle;

        public override string[] KeywordsList => new string[] { 
            "Martin Drozdík",
            "Software engineer",
            "OOP",
            "programátor",
            "portfolio",
            "osobní web",
            "projekty",
            "blog"
        };

        public override string OgImage => "/Web/_Pages/IndexPage/IndexOG.png";

        public override PageId PageId => PageId.Index;

        
    }
}
