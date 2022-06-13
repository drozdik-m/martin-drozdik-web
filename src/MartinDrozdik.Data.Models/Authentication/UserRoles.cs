using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MartinDrozdik.Data.Models.Authentication
{
    public static class UserRoles
    {
        public const string Developer = "developer";

        public const string Admin = "admin";

        /// <summary>
        /// Roles that may be shared with the world
        /// </summary>
        public static readonly IEnumerable<string> ExposedRoles = new string[]
        {
            Developer,
            Admin
        };

        /// <summary>
        /// Checks if a role is exposed
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool IsExposed(string role) => ExposedRoles?.Contains(role) ?? false;

        /// <summary>
        /// Checks if a role from a claim is exposed
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public static bool IsExposed(Claim claim)
        {
            if (claim == null)
                return false;

            return claim.Type == ClaimTypes.Role && IsExposed(claim.Value);
        }

        /// <summary>
        /// Returns list separated by ","
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static string List(params string[] roles) => string.Join(",", roles);

        /// <summary>
        /// Checks if a list of claims has a role
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool HasRoleClaim(this IList<Claim> claims, string role)
            => claims?.Where(e => e.Type == ClaimTypes.Role)
                     .Where(e => e.Value == role)
                     .Any() ?? false;
    }
}
