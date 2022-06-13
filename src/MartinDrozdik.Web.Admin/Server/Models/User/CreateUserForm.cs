using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Web.Admin.Server.Models.User
{
    public class CreateUserForm
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
