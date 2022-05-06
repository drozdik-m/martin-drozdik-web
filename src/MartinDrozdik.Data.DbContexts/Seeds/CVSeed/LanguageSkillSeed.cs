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
    public class LanguageSkillSeed : ISeed
    {
        private readonly AppDb context;

        public LanguageSkillSeed(AppDb context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            //Create items
            var items = new List<LanguageSkill>()
            {
                new LanguageSkill()
                {
                    LanguageName = "Český jazyk",
                    SkillLevel = LanguageSkillLevel.Native,
                    OrderIndex = 0,
                },
                new LanguageSkill()
                {
                    LanguageName = "Anglický jazyk",
                    SkillLevel = LanguageSkillLevel.C1,
                    OrderIndex = 1,
                },
                new LanguageSkill()
                {
                    LanguageName = "Německý jazyk",
                    SkillLevel = LanguageSkillLevel.A2,
                    OrderIndex = 2,
                },
            };

            //Add and save items
            foreach(var item in items)
                context.Add(item);
            await context.SaveChangesAsync();
        }


    }
}
