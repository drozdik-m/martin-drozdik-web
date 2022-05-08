using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Abstraction.Entities
{
    public interface IOrderable
    {
        /// <summary>
        /// Order index of this entity
        /// </summary>
        int OrderIndex { get; set; }
    }
}
