using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Data.DbContexts.Seeds
{
    public class SeedLoader
    {
        private readonly IHost host;

        public SeedLoader(IHost host)
        {
            this.host = host;
        }

        public async Task LoadSeed<TSeed>()
            where TSeed : ISeed
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var seed = services.GetService<TSeed>();

                await seed.SeedAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding the database", ex);
            }
        }
    }
}
