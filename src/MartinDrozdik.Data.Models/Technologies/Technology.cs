using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Technologies
{
    public class Technology : EntityBase, IIdentifiable<int>, IOrderable
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Very short description of the technology (fe. "Front-end SPA framework")
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc/>
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// Profile picture of the person
        /// </summary>
        public TechnologyLogo Logo { get; set; } = new TechnologyLogo();

        /// <summary>
        /// Id of the profile picture object
        /// </summary>
        [ForeignKey("Logo")]
        public int LogoId { get; set; }

        /// <inheritdoc/>
        public override string ToString() => Name;
    }
}
