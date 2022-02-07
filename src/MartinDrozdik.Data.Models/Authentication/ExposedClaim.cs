using System;
using System.Collections.Generic;
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
