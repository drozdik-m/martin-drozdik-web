using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Models
{
    /// <summary>
    /// Base class for all entities
    /// </summary>
    public class EntityBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime LastEditAt { get; set; } = DateTime.Now;
    }
}
