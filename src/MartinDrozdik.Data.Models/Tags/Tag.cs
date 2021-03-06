using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Models;

namespace MartinDrozdik.Data.Models.Tags
{
    public class Tag: EntityBase, IIdentifiable<int>, IOrderable
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int OrderIndex { get; set; } = 0;

        public override string ToString() => $"{Name}";
    }
}
