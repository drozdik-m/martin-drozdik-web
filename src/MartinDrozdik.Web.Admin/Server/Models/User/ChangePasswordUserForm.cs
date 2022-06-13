using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MartinDrozdik.Web.Admin.Server.Models.User
{
    public class ChangePasswordUserForm
    {
        public string OldPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;

        public string ConfirmNewPassword { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
