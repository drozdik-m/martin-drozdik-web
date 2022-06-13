using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MartinDrozdik.Data.Models.Authentication
{
    public class AuthenticationStateData
    {
        /// <summary>
        /// True if the user is authenticated, else false
        /// </summary>
        public bool IsAuthenticated { get; set; } = false;

        /// <summary>
        /// Name of the user
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// List of exposed claims
        /// </summary>
        public List<ExposedClaim> ExposedClaims { get; set; } = new List<ExposedClaim>();

        /// <summary>
        /// Checks if one of the claims contains a role with a name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool HasRole(string roleName)
            => ExposedClaims
            .Where(e => e.Type == ClaimTypes.Role)
            .Where(e => e.Value == roleName)
            .Any();

        /// <summary>
        /// Returns instance of state data belonging to no user
        /// </summary>
        /// <returns></returns>
        public static AuthenticationStateData NoUser { get; } = new AuthenticationStateData
        {
            IsAuthenticated = false
        };
    }
}
