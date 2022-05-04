using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.CV
{
    public class Education : EntityBase, IIdentifiable<int>, IOrderable
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// Name of the education - name of the school, title, ...
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// What field of study?
        /// </summary>
        public string Specialization { get; set; } = string.Empty;

        /// <summary>
        /// Start time of the education
        /// </summary>
        public DateTime StartedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Tells if the education is concluded
        /// </summary>
        public bool Finished { get; set; } = false;

        /// <summary>
        /// End time of the education
        /// </summary>
        public DateTime EndedDate { get; set; } = DateTime.Now;

    }
}
