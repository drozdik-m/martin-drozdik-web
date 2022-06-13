using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Abstraction.Services
{
    public interface ISeedableService
    {
        /// <summary>
        /// Executes the seeding procedure for a resource
        /// </summary>
        /// <returns></returns>
        Task SeedAsync();
    }
}
