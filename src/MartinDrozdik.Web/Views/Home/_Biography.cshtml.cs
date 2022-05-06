using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.CV;

namespace MartinDrozdik.Web.Views.Home
{
    public class BiographyModel
    {
        public IEnumerable<WorkExperience> WorkExperiences { get; }
        public IEnumerable<Education> Educations { get; }
        public IEnumerable<LanguageSkill> LanguageSkills { get; }

        public BiographyModel(IEnumerable<WorkExperience> workExperiences,
            IEnumerable<Education> educations,
            IEnumerable<LanguageSkill> languageSkills)
        {
            WorkExperiences = workExperiences;
            Educations = educations;
            LanguageSkills = languageSkills;
        }
    }
}
