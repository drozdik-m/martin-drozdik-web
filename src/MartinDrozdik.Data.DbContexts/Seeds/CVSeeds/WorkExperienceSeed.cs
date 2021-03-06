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

namespace MartinDrozdik.Data.DbContexts.Seeds.CVSeeds
{
    public class WorkExperienceSeed : DbSeed
    {
        public WorkExperienceSeed(AppDb context) : base(context)
        {
        }

        public override async Task SeedAsync()
        {
            //Create items
            var items = new List<WorkExperience>()
            {
                new WorkExperience()
                {
                    WorkPosition = "Spoluzakladatel a full-stack programátor",
                    PlaceOfWork = "Bonsai Development",
                    PlaceOfWorkHasWebsite = true,
                    PlaceOfWorkUri = "https://bonsai-development.cz/",
                    Description = "Popisem mé práce je tvorba a optimalizace webových stránek, administrací, webových aplikací, content management systémů, databází a následná správa v produkčním běhu. Mimo jiné i organizuji tým, komunikuji s klienty a věnuji se návrhu uživatelského prostředí se správnou přístupností dle byznysových potřeb.",
                    StartedDate = new DateTime(2016, 3, 1),
                    Ended = false,
                    //EndedDate = new DateTime(2019, 9, 1),
                    OrderIndex = 0,
                },
                new WorkExperience()
                {
                    WorkPosition = "Vyučující předmětu OOP na ČVUT",
                    PlaceOfWork = "ČVUT",
                    PlaceOfWorkHasWebsite = true,
                    PlaceOfWorkUri = "https://fit.cvut.cz/",
                    Description = "Na ČVUT vedu cvičení předmětu BI-OOP, jež se zaměřuje na správný objektově-orientovaný návrh a výuku teorie spojené s tímto paradigmatem. Předmět byl v roce mého nástupu v novém kabátě, tudíž jsem se aktivně podílel na jeho vývoji. V roce 2021 jsem obdržel a úspěšně dokončil grant na kompletní předělání laboratorních osnov.",
                    StartedDate = new DateTime(2020, 9, 1),
                    Ended = false,
                    //EndedDate = new DateTime(2019, 9, 1),
                    OrderIndex = 1,
                },
                new WorkExperience()
                {
                    WorkPosition = "Stáž jako .NET a front-end developer",
                    PlaceOfWork = "ICZ",
                    PlaceOfWorkHasWebsite = true,
                    PlaceOfWorkUri = "https://www.iczgroup.com/",
                    Description = "Popisem mé práce byl výzkum nového .NET SPA frameworku Blazor a proof of concept implementace modernější podoby správy uživatelů české evidence řidičů.",
                    StartedDate = new DateTime(2019, 6, 1),
                    Ended = true,
                    EndedDate = new DateTime(2019, 9, 1),
                    OrderIndex = 2,
                },
                new WorkExperience()
                {
                    WorkPosition = "Zahraniční stáž jako ASP.NET developer",
                    PlaceOfWork = "In 2 Solid Surfaces",
                    PlaceOfWorkHasWebsite = true,
                    PlaceOfWorkUri = "https://in2solidsurfaces.co.uk/",
                    Description = "Zahraniční stáž programu Erasmus+ mně umožnila žít na měsíc v Anglii a vytvářet nové webové stránky pro lokální podnik. Díky programování redakčního systému v ASP.NET (bez šablonových řešení) firma ušetřila na měsíčních nákladech za různé softwarové služby, jelikož byly nahrazeny mojí vlastní implementací.",
                    StartedDate = new DateTime(2016, 6, 1),
                    Ended = true,
                    EndedDate = new DateTime(2016, 7, 1),
                    OrderIndex = 3,
                },

            };

            //Add and save items
            foreach (var item in items)
                Context.Add(item);
            await Context.SaveChangesAsync();
        }


    }
}
