using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Web.Admin.Server.Models.User
{
    public class LoginUserForm
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
