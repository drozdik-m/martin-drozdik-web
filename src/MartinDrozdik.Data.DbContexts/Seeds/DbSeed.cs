using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Data.DbContexts.Seeds
{
    public abstract class DbSeed : ISeed
    {
        protected AppDb Context { get; }

        public DbSeed(AppDb context)
        {
            Context = context;
        }

        /// <inheritdoc />
        public abstract Task SeedAsync();
    }
}
