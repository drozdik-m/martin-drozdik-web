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

        public string LanguageSkillLevelText(LanguageSkillLevel languageSkillLevel)
        {
            return languageSkillLevel switch
            {
                LanguageSkillLevel.A1 => "Úroveň A1 (začátečník)",
                LanguageSkillLevel.A2 => "Úroveň A2 (základy)",
                LanguageSkillLevel.B1 => "Úroveň B1 (středně pokročilý)",
                LanguageSkillLevel.B2 => "Úroveň B2 (pokročilý)",
                LanguageSkillLevel.C1 => "Úroveň C1 (velmi pokročilý)",
                LanguageSkillLevel.C2 => "Úroveň C2 (zběhlý)",
                LanguageSkillLevel.Native => "Rodný jazyk",
                _ => throw new Exception("Unknown language skill level"),
            };
        }

        public string EducationTimeRangeText(Education education)
        {
            var firstDate = education.StartedDate.Value.Year.ToString();
            var secondDate = education.EndedDate.HasValue
                ? education.EndedDate.Value.Year.ToString()
                : "současnost";
            return $"{firstDate} – {secondDate}";
        }

        public string WorkExperienceTimeRangeText(WorkExperience workExperience)
        {
            var firstDate = WorkExperienceDateText(workExperience.StartedDate.Value);
            var secondDate = workExperience.Ended && workExperience.EndedDate.HasValue 
                ? WorkExperienceDateText(workExperience.EndedDate.Value)
                : "současnost";
            return $"{firstDate} – {secondDate}";
        }

        public string WorkExperienceDateText(DateTime dateTime)
        {
            var month = dateTime.Month switch
            {
                1  => "leden",
                2  => "únor",
                3  => "březen",
                4  => "duben",
                5  => "květen",
                6  => "červen",
                7  => "červenec",
                8  => "srpen",
                9  => "září",
                10 => "říjen",
                11 => "listopad",
                12 => "prosinec",
                _ => throw new Exception("Unknown month index")
            };

            var year = dateTime.Year.ToString();

            return $"{month} {year}";
        }
    }
}
