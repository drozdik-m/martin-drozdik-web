using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Data.DbContexts.Seeds
{
    public interface ISeed
    {
        /// <summary>
        /// Does the seeding operation
        /// </summary>
        /// <returns>True if seeding was required, else false</returns>
        Task SeedAsync();
    }
}
