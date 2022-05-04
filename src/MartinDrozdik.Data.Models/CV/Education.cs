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
        public int OrderIndex { get; set; }

        //public string 
    }
}
