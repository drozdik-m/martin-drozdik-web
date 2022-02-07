using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.UserIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bonsai.Server.Controllers.Localization
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        async Task<AuthenticationStateData> GetAuthStateDataFor(AppUser user)
        {

            var state = new AuthenticationStateData
            {
                IsAuthenticated = User?.Identity?.Name == user.UserName,
                UserName = user.UserName
            };

            return await Task.FromResult(state);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthenticationStateData>>> GetAsync()
        {
            var users = userManager.Users.ToList();
            var results = new List<AuthenticationStateData>();
            foreach (var user in users)
                results.Add(await GetAuthStateDataFor(user));
            return Ok(results);
        }

        [AllowAnonymous]
        [HttpGet("me")]
        public async Task<AuthenticationStateData> MeAsync()
        {
            //Check if any user logged in
            if (User?.Identity?.Name == null)
                return AuthenticationStateData.NoUser();

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            //Check if the user exists
            if (user == null)
                return AuthenticationStateData.NoUser();

           
            var res = new AuthenticationStateData
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name
            };

            return res;
        }
    }
}
