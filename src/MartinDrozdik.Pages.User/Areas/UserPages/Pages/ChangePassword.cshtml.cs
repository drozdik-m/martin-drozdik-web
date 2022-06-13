using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bonsai.RazorPages.User.Services.LanguageDictionary;
using MartinDrozdik.Services.FilePathProvider;
using Microsoft.AspNetCore.Authorization;

namespace Bonsai.RazorPages.User.Areas.UserPages.Pages
{
    [Authorize]
    public class ChangePasswordModel : UserPagesLayoutModel
    {
        public ChangePasswordModel(IUserLanguageDictionary languageDictionary, IFilePathProvider pathProvider)
        {
            LanguageDictionary = languageDictionary;
            PathProvider = pathProvider;
        }

        public override string Title => LanguageDictionary.GetContent(UserLanguageDictionary.ChangePasswordHeading);

        public override string Description => "Change password page";

        public override string Keywords => "Change password";

        public string Heading => LanguageDictionary.GetContent(UserLanguageDictionary.ChangePasswordHeading);

        public string ChangePasswordButton => LanguageDictionary.GetContent(UserLanguageDictionary.ChangePasswordButton);

        public string OldPasswordLabel => LanguageDictionary.GetContent(UserLanguageDictionary.OldPassword);
        public string OldPasswordNotEmpty => LanguageDictionary.GetContent(UserLanguageDictionary.OldPasswordNotEmpty);

        public string NewPasswordLabel => LanguageDictionary.GetContent(UserLanguageDictionary.NewPasswordLabel);
        public string NewPasswordNotEmpty => LanguageDictionary.GetContent(UserLanguageDictionary.NewPasswordNotEmpty);

        public string ConfirmNewPasswordLabel => LanguageDictionary.GetContent(UserLanguageDictionary.ConfirmNewPasswordLabel);
        public string ConfirmNewPasswordSame => LanguageDictionary.GetContent(UserLanguageDictionary.ConfirmNewPasswordSame);

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = "")
        {
            ReturnUrl = returnUrl;
        }
    }
}
