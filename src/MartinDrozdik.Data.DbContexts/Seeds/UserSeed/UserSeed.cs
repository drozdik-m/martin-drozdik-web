using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            {
                var user = new AppUser
                {
                    UserName = userConfig.Email,
                    Email = userConfig.Email,
                    PhoneNumber = userConfig.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };

                //Add the users
                await AddUser(user, userConfig.Password);

                //Add the roles
                foreach(var role in userConfig.Roles)
                    await AddRole(user, role);
            }
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

        async Task AddRole(AppUser user, string roleName)
        {
            var claims = await userManager.GetClaimsAsync(user);
            var existingRoleClaims = claims
                .Where(e => e.Type == ClaimTypes.Role)
                .Where(e => e.Value == roleName);

            if (!existingRoleClaims.Any())
            

            

            //await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, roleName));


        }
    }
}
