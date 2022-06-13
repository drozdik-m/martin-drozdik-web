using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Abstraction.Entities
{
    public interface IHideable
    {
        /// <summary>
        /// Idicates whether this entity is hidden or not
        /// </summary>
        bool IsHidden { get; set; }
    }
}
