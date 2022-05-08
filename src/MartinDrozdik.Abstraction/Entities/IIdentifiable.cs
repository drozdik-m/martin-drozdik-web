using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Abstraction.Entities
{
    public interface IIdentifiable<TKey>
    {
        /// <summary>
        /// Identifier of the entity
        /// </summary>
        TKey Id { get; set; }
    }
}
