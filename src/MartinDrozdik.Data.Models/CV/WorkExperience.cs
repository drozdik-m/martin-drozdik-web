using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.CV
{
    public class WorkExperience : EntityBase, IIdentifiable<int>, IOrderable
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// Name of the work
        /// </summary>
        public string WorkPosition { get; set; } = string.Empty;

        /// <summary>
        /// Where the work was conducted
        /// </summary>
        public string PlaceOfWork { get; set; } = string.Empty;

        /// <summary>
        /// Tells if the place of work has a website
        /// </summary>
        public bool PlaceOfWorkHasWebsite { get; set; } = false;

        /// <summary>
        /// If the company / place of work has a website, this is the link
        /// </summary>
        public string PlaceOfWorkUri { get; set; } = string.Empty;

        /// <summary>
        /// The description of the work
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Start time of the work experience
        /// </summary>
        public DateTime StartedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Tells if the work experience is concluded
        /// </summary>
        public bool Finished { get; set; } = false;

        /// <summary>
        /// End time of the work experience
        /// </summary>
        public DateTime EndedDate { get; set; } = DateTime.Now;

    }
}
