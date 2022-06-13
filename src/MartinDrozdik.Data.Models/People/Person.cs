using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.People
{
    public class Person : EntityBase, IIdentifiable<int>, IOrderable
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Is this person me? Is this Martin Drozdík?
        /// </summary>
        public bool IsMe { get; set; } = false;

        /// <inheritdoc/>
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// Profile picture of the person
        /// </summary>
        public PersonProfileImage ProfileImage { get; set; } = new PersonProfileImage();

        /// <summary>
        /// Id of the profile picture object
        /// </summary>
        [ForeignKey("ProfileImage")]
        public int ProfileImageId { get; set; }

        public override string ToString() => Name;
    }
}
