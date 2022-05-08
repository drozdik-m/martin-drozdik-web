using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Abstraction.Entities
{
    public interface INameable
    {
        /// <summary>
        /// Name of this entity
        /// </summary>
        string Name { get; set; }
    }
}
