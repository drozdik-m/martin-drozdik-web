using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.CV
{
    public class LanguageSkill : EntityBase, IIdentifiable<int>, IOrderable
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// Name of the langauge
        /// </summary>
        public string LanguageName { get; set; } = string.Empty;

        /// <summary>
        /// The level of skill for this language
        /// </summary>
        public LanguageSkillLevel SkillLevel { get; set; } = LanguageSkillLevel.A1;
    }
}
