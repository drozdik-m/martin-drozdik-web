using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Data.DbContexts.Configuration
{
    public class SeedUserConfiguration
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
