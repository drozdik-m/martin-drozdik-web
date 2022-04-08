using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.UserIdentity;
using MartinDrozdik.Web.Admin.Server.Models.User;
using MartinDrozdik.Web.Facades.User;
using MartinDrozdik.Web.Facades.User.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles = UserRoles.Developer + "," + UserRoles.Admin)]
    public class UserApiController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UserApiController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("me")]
        public async Task<AuthenticationStateData> MeAsync()
        {
            if (User?.Identity?.Name == null)
                return AuthenticationStateData.NoUser;

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return AuthenticationStateData.NoUser;

            var claims = await userManager.GetClaimsAsync(user) ?? new List<Claim>();

            return new AuthenticationStateData
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                ExposedClaims = claims
                    .Where(c => UserRoles.IsExposed(c))
                    .Select(c => new ExposedClaim { Type = c.Type, Value = c.Value }).ToList()
            };
        }
    }
}
