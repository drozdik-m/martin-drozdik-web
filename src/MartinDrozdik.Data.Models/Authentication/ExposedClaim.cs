using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MartinDrozdik.Data.Models.Authentication
{
    public class ExposedClaim
    {
        public ExposedClaim()
        {

        }

        public ExposedClaim(string type, string value)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            if (type == ClaimTypes.Role && !UserRoles.IsExposed(value))
                throw new ArgumentException($"Role claim {value} is not exposed");

            Type = type;
            Value = value;  
        }

        /// <summary>
        /// Type of the claim
        /// </summary>
        public string Type { get; set; } = string.Empty;


        /// <summary>
        /// Value of the claim
        /// </summary>
        public string Value { get; set; } = string.Empty;
    }
}
