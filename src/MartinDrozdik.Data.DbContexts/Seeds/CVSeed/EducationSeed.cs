using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.DbContexts.Configuration;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.UserIdentity;
using Microsoft.AspNetCore.Identity;

namespace MartinDrozdik.Data.DbContexts.Seeds.CVSeed
{
    public class EducationSeed : ISeed
    {
        private readonly AppDb context;

        public EducationSeed(AppDb context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            //Create items
            var items = new List<Education>()
            {
                new Education()
                {
                    Name = "ČVUT FIT v Praze (Ing.)",
                    Specialization = "Softwarové inženýrstvý",
                    StartedDate = new DateTime(2020, 9, 0),
                    Ended = false,
                    EndedDate = new DateTime(2022, 6, 1),
                    OrderIndex = 0,
                },
                new Education()
                {
                    Name = "ČVUT FIT v Praze (Bc.)",
                    Specialization = "Softwarové inženýrstvý",
                    StartedDate = new DateTime(2017, 9, 0),
                    Ended = true,
                    EndedDate = new DateTime(2020, 6, 1),
                    OrderIndex = 1,
                },
                new Education()
                {
                    Name = "SPŠSE a VOŠ Liberec",
                    Specialization = "Technické lyceum",
                    StartedDate = new DateTime(2013, 9, 0),
                    Ended = true,
                    EndedDate = new DateTime(2017, 6, 1),
                    OrderIndex = 2,
                },
                new Education()
                {
                    Name = "ZŠ Doctrina",
                    StartedDate = new DateTime(2004, 9, 0),
                    Ended = true,
                    EndedDate = new DateTime(2013, 6, 1),
                    OrderIndex = 3,
                },

            };

            //Add and save items
            foreach(var item in items)
                context.Add(item);
            await context.SaveChangesAsync();
        }


    }
}
