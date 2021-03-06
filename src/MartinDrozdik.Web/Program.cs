using MartinDrozdik.Data.DbContexts.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MartinDrozdik.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Create the host
            var host = CreateHostBuilder(args).Build();

            //Run the server
            host.Run();
        }

        /// <summary>
        /// Creates the app host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
