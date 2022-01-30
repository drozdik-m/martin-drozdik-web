using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.DbContexts.Configuration;
using MartinDrozdik.Data.Models.UserIdentity;
using Microsoft.AspNetCore.Identity;

namespace MartinDrozdik.Data.DbContexts.Seeds.UserSeed
{
    public class UserSeed : ISeed
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SeedUserConfiguration[] seedUsers;

        public UserSeed(UserManager<AppUser> userManager,
             SeedUserConfiguration[] seedUsers)
        {
            this.userManager = userManager;
            this.seedUsers = seedUsers;
        }

        public async Task SeedAsync()
        {
            foreach(var userConfig in seedUsers)
                await AddUser(
                    new AppUser { 
                        UserName = userConfig.Email, 
                        Email = userConfig.Email,
                        PhoneNumber = userConfig.PhoneNumber,
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true
                    }, 
                    userConfig.Password);
        }

        async Task AddUser(AppUser user, string password)
        {
            var searchResult = await userManager.FindByEmailAsync(user.Email);

            //The user already exists
            if (searchResult != null)
                return;
                
            //Create the user
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.ToString());
        }
    }
}
